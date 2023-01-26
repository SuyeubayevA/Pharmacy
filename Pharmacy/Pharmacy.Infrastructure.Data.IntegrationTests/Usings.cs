global using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Data.Repositories;
using System.Web.Http.Controllers;

var services = new ServiceCollection();
services.AddScoped<DbContext, PharmacyDBContext>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddTransient<BaseRepository<Product>, ProductRepository>();