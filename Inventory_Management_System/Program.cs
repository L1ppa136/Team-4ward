using Inventory_Management_System;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UseSqlServer
builder.Services.AddDbContext<InventoryManagementDBContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433;Database=InventoryManagementSystem;User Id=sa;Password=TEAM4W@rd;Encrypt=False;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryManagementDBContext>();
    dbContext.Database.Migrate();
}

app.MapControllers();
app.Run();
