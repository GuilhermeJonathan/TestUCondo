using Microsoft.EntityFrameworkCore;
using TestUCondo.Domain.Entities;

namespace TestUCondo.Infra.Data.Context
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        public DbSet<User> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
