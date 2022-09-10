using LocalsAPISG.Entites;
using Microsoft.EntityFrameworkCore;

namespace LocalsAPISG.Entities
{
    public class LocalsDbContext : DbContext
    {
        private string _connectionString = "Server=LAPTOP-75I5RVU3;Database=LocalsDb;Trusted_Connection=True";
        public DbSet<Locals> Locals { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();


            modelBuilder.Entity<Locals>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Menu>()
                .Property(d => d.Name)
                .IsRequired();
            /*.HasMaxLength(25);

        modelBuilder.Entity<Address>()
            .Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Address>()
            .Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(50);*/
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
