using GData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GData.Data
{
    public class GDataDbContext(DbContextOptions<GDataDbContext> options):DbContext(options)
    {

        public DbSet<User> Users => Set<User>();

        public DbSet<Post> Posts => Set<Post>();

        public DbSet<PostComment> PostComments => Set<PostComment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("User ID=postgres;Password=Bit_2024;Host=localhost;Port=5432;Database=GDataDb;Pooling=true;Connection Lifetime=30;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasOne(p=>p.Owner).WithMany(u=>u.UserPosts).HasForeignKey(p=>p.OwnerId);

            modelBuilder.Entity<PostComment>().HasOne(pc=>pc.Author).WithMany(au=>au.PostComments).HasForeignKey(pc=>pc.AuthorId);

            modelBuilder.Entity<PostComment>().HasOne(pc=>pc.Post).WithMany(p=>p.PostComments).HasForeignKey(pc=>pc.PostId);

        }

    }
}
