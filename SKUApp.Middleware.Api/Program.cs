
using SKUApp.Data.EFCore.InMemory;
using SKUApp.Domain.DataContracts;
using SKUApp.Domain.ServiceContracts;
using SKUApp.Domain.Services;
using SKUApp.Middleware.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InMemoryDbContext>();
builder.Services.AddScoped<ISKUConfigService, SKUConfigService>();
builder.Services.AddScoped<ISKUPartConfigService, SKUPartConfigService>();
builder.Services.AddScoped<ISKUService, SKUService>();
builder.Services.AddScoped<ISKUUnitOfWork, InMemorySKUUnitOfWork>();

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