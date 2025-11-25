using GData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GData.Data
{
    public class GDataDbContext(DbContextOptions<GDataDbContext> options):DbContext(options)
    {

        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("User ID=postgres;Password=Bit_2024;Host=localhost;Port=5432;Database=GDataDb;Pooling=true;Connection Lifetime=30;");

        }

    }
}
