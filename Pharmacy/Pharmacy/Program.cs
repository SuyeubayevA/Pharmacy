using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pharmacy.Helpers;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Infrastructure.Handlers.ProductQueriesHanders;
using Pharmacy.Profiles;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var mapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new ProductMappingProfile());
        cfg.AddProfile(new ProductToDTOMappingProfile());
    }
);
var mapper = mapperConfig.CreateMapper();

IConfiguration configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", true, true)
   .Build();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<PharmacyDBContext>(
     options => options.UseSqlServer(configuration.GetConnectionString("pharmacyDB"))
    );

builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PharmacyAPI_01",
        Version = "v1",
    });
});
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetAllProductsHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(c => c
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PharmacyAPI_01 v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program
{

}