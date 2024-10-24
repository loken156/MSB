using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database
{
    public class MSB_DatabaseFactory : IDesignTimeDbContextFactory<MSB_Database>
    {
        public MSB_Database CreateDbContext(string[] args)
        {
            // Adjust the path to point to the API project's directory
            // ändra blackslash i  \API
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../API"));

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<MSB_Database>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 21)),
                mysqlOptions => mysqlOptions.EnableRetryOnFailure());


            return new MSB_Database(optionsBuilder.Options);
        }
    }
}