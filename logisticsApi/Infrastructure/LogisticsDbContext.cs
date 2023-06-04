
using logisticsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace logisticsApi.Infrastructure
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options): base(options)
        {
        }
        // Agregar todos los modelos aqui

        public DbSet<Bodegas> bodegas { get; set; }
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<TiposProductos> tiposProductos { get; set; }
        public DbSet<Puertos> puertos { get; set; }
        public DbSet<LogisticaTerrestre> logisticaTerrestre { get; set; }

        public DbSet<LogisticaMaritima> logisticaMaritima { get; set; }
        public DbSet<Usuarios> usuarios { get; set; }

    }
}
