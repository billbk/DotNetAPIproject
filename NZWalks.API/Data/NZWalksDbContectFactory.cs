using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NZWalks.API.Data
{
    public class NZWalksDbContextFactory : IDesignTimeDbContextFactory<NZWalksDbContext>
    {
        public NZWalksDbContext CreateDbContext(string[] args)
        {
            // ✅ TEMPORARY: Hardcode connection string directly to test
            var connectionString = "Server=localhost;Database=NZWalksDb;Trusted_Connection=True;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<NZWalksDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new NZWalksDbContext(optionsBuilder.Options);
        }
    }
}
