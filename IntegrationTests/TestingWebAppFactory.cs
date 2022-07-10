using CoffeeClub;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace IntegrationTests
{
    public class TestingWebAppFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CoffeeClubContext>));

                if (dbContext != null)
                    services.Remove(dbContext);

                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CoffeeClubContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    using (var appContext = scope.ServiceProvider.GetRequiredService<CoffeeClubContext>())
                    {
                        try
                        {
                            appContext.Database.EnsureCreated();
                        }
                        catch (Exception ex)
                        {
                            //Log errors
                            throw;
                        }
                    }
                }
            });
        }
    }
}
