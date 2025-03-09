using SKUApp.Domain.Infrastructure.EntityFramework.InMemory;
using SKUApp.Domain.Infrastructure.Services;
using SKUApp.Domain.Infrastructure.UnitOfWork;
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
app.MapSKUPartConfigEndpoints();
app.MapSKUPartEntryEndpoints();

app.Run();
