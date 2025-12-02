using Microsoft.EntityFrameworkCore;
using Residencias.API.Servicos;

namespace Residencias.API
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=residencias.db");
        }

        public DbSet<Residencia> Residencias { get; set; }
    }
}