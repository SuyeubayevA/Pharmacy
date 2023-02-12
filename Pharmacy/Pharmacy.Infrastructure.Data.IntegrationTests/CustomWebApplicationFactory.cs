using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Infrastructure.Data.Abstracts;
using System.Configuration;

namespace Pharmacy.Infrastructure.Data.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> :
             WebApplicationFactory<TStartup> where TStartup : class
    {
        public CustomWebApplicationFactory()
        {
        }

        public PharmacyDBContext DBContext { get; set; }
        public IUnitOfWork UOW { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault
                   (d => d.ServiceType == typeof(DbContextOptions<PharmacyDBContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<PharmacyDBContext>
                  (
                    options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PharmacyPDP;Integrated Security=True;")
                  );

                services.AddScoped<PharmacyDBContext>();

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();


                DBContext = serviceProvider.GetRequiredService<PharmacyDBContext>();
                var testUOW = serviceProvider.GetRequiredService<UnitOfWork>();

                UOW = serviceProvider.GetRequiredService<IUnitOfWork>();
            });
        }
    }
}
