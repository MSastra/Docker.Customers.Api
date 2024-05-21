using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL($"server=localhost;port=3306;user={Environment.GetEnvironmentVariable("MYSQL_USER")};password={Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD")};database={Environment.GetEnvironmentVariable("MYSQL_DATABASE")}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
