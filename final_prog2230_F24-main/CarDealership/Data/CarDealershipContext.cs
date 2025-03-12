using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CarDealership.Data;

public class CarDealershipContext : DbContext
{
    public CarDealershipContext(DbContextOptions<CarDealershipContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Set the database file path in the Data directory
            var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            var dbPath = Path.Combine(dataDirectory, "CarDealership.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }


}
