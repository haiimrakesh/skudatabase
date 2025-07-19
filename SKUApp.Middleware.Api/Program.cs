
using Microsoft.EntityFrameworkCore;
using SKUApp.Data.EFCore.InMemory;
using SKUApp.Data.EFCore.SqlServer;
using SKUApp.Domain.DataContracts;
using SKUApp.Domain.ServiceContracts;
using SKUApp.Domain.Services;
using SKUApp.Middleware.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SqlServerDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<ISKUConfigService, SKUConfigService>();
builder.Services.AddScoped<ISKUPartConfigService, SKUPartConfigService>();
builder.Services.AddScoped<ISKUService, SKUService>();
builder.Services.AddScoped<ISKUUnitOfWork, SqlServerSKUUnitOfWork>();
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapSKUConfigEndpoints();
//app.MapSKUPartConfigEndpoints();
//app.MapSKUPartEntryEndpoints();

app.Run();

public partial class Program
{
    // This partial class is used to separate the main method from the rest of the code.
    // It allows for better organization and maintainability of the code.
}