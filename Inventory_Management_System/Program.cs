using Inventory_Management_System.Data;
using Inventory_Management_System.Service.Authentication;
using Inventory_Management_System.Service.Repositories;
using Inventory_Management_System.Model.Location;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        AddServices();
        ConfigureSwagger();
        AddDbContext(configuration);
        AddAuthentication();
        AddIdentity();

        var app = builder.Build();

//Adding all roles to AspNetRoles
        AddRoles();

//Add admin if not exists
        AddAdmin();

//Migrate InventoryManagementDBContext
        DBMigration();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
// Add CORS middleware here
        app.UseCors(builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                ;
        });

        app.UseHttpsRedirection();

//Authentication and Authorization
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();

        void DBMigration()
        {
            // migrate any database changes on startup (includes initial db creation)
            using (var scope = app.Services.CreateScope())
            {
                var inventoryContext = scope.ServiceProvider.GetRequiredService<InventoryManagementDBContext>();
                inventoryContext.Database.Migrate();
            }
        }

        void AddServices()
        {
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
        }

        void ConfigureSwagger()
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }

                    }
                });
            });
        }

        void AddDbContext(IConfiguration configuration)
        {
            builder.Services.AddDbContext<InventoryManagementDBContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));    
            });

            builder.Services.AddDbContext<UsersContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));    
            });

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IProduction, LogisticService>();
            builder.Services.AddScoped<IStock, LogisticService>();
            builder.Services.AddScoped<ISupplier, LogisticService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
        }    


        void AddAuthentication()
        {
            //This will add a JWT token authentication scheme to your API. This piece of code is required to validate a JWT.
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "apiWithAuthBackend",
                        ValidAudience = "apiWithAuthBackend",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("!SomethingSecret!")
                
                    
                        ),
                
                    };
                });
        }

        void AddIdentity()
        {
            //User requirements
            builder.Services
                .AddIdentityCore<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UsersContext>();
        }

        void AddRoles()
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var tAdmin = CreateAdminRole(roleManager);
            tAdmin.Wait();

            var tUser = CreateUserRole(roleManager);
            tUser.Wait();

            var tCustomerPlanner = CreateCustomerPlannerRole(roleManager);
            tCustomerPlanner.Wait();

            var tForkliftDriver = CreateForkliftdriverRole(roleManager);
            tForkliftDriver.Wait();

            var tProductionLeader = CreateProductionLeaderRole(roleManager);
            tProductionLeader.Wait();

            var tWareouseLeader = CreateWarehouseLeaderRole(roleManager);
            tWareouseLeader.Wait();
        }

        async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        async Task CreateCustomerPlannerRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Customer Planner"));
        }

        async Task CreateForkliftdriverRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Forklift Driver"));
        }

        async Task CreateProductionLeaderRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Production Leader"));
        }

        async Task CreateWarehouseLeaderRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Warehouse Leader"));
        }

        void AddAdmin()
        {
            var tAdmin = CreateAdminIfNotExists();
            tAdmin.Wait();
        }

        async Task CreateAdminIfNotExists()
        {
            using var scope = app.Services.CreateScope();
            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var adminInDb = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminInDb == null)
            {
                var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
                var adminCreated = await userManager.CreateAsync(admin, "Admin123");

                if (adminCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
