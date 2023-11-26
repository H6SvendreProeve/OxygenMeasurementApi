using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OxygenMeasurementApi.Data.Context;
using Testcontainers.PostgreSql;

namespace OxygenMeasurementApi.Tests;

// this class is used to make an in memory version of OxygenMeasurementApi with an implementation of PostgreSqlContainer for database
public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer postgresSqlTestContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("OxygenMeasurementTestDb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>

            {
                var descriptor = services.SingleOrDefault(serviceDescriptor =>
                    serviceDescriptor.ServiceType == typeof(DbContextOptions<OxygenDbContext>));

                if (descriptor is not null)
                {
                    // remove the default OxygenDbContext if its configured.
                    services.Remove(descriptor);
                }
                
                // add a new postgres OxygenDbContext with the postgresSqlTestContainers connectionString
                services.AddDbContext<OxygenDbContext>(options =>
                    options.UseNpgsql(postgresSqlTestContainer.GetConnectionString())
                );
                
            }
        );
    }
    
    // start the postgresSqlTestContainer
    public Task InitializeAsync()
    {
        return postgresSqlTestContainer.StartAsync();
    }
    
    // stop / dispose the postgresSqlTestContainer
    public new Task DisposeAsync()
    {
        return postgresSqlTestContainer.StopAsync();
    }
}