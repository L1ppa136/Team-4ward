using Inventory_Management_System.Data;
using Inventory_Management_System.Service.Authentication;
using Inventory_Management_System.Service.Repositories;
using Inventory_Management_System.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
string? connectionString = _configuration.GetConnectionString("Default");
Console.WriteLine(connectionString);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UseSqlServer
builder.Services.AddDbContext<InventoryManagementDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContext<UsersContext>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProduction, LogisticService>();
builder.Services.AddScoped<IStock, LogisticService>();
builder.Services.AddScoped<ISupplier, LogisticService>();
builder.Services.AddScoped<ITokenService, TokenService>();

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
    })
    .AddEntityFrameworkStores<UsersContext>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

//Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();


//SEEMS LIKE DUPLICATE MIGRATIONS AND APP DO NOT RUN
// Apply migrations
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryManagementDBContext>();
//    dbContext.Database.Migrate();
//}

app.MapControllers();
app.Run();
