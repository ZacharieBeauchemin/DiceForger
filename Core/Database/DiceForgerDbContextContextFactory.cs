using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Core.Database;

public class DiceForgerDbContextFactory: IDesignTimeDbContextFactory<DiceForgerDbContext> {
    private static IConfigurationRoot? _configuration;

    private static IConfigurationRoot Configuration {
        get {
            if(_configuration is null) {
                _configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetCallingAssembly()!.Location))
                    .AddJsonFile("appsettings.json")
                    .Build();
            }

            return _configuration; 
        }
        set {
            _configuration = value;
        }
    }

    public DiceForgerDbContext CreateDbContext(string[] args) {
        return CreateDbContext();
    }

    public static DiceForgerDbContext CreateDbContext() {
        DbContextOptionsBuilder<DiceForgerDbContext> dbContextBuilder = new();

        string connectionString = Configuration.GetConnectionString("DefaultConnection");

        dbContextBuilder.UseNpgsql(connectionString);

        return new DiceForgerDbContext(dbContextBuilder.Options);

    }
}