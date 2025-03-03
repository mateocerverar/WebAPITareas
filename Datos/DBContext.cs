using Datos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Tarea> Tareas => Set<Tarea>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasIndex(t => t.Codigo).IsUnique();
                entity.Property(t => t.FechaCreacion).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
