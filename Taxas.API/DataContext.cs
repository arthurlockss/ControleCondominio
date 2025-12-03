using Microsoft.EntityFrameworkCore;
using Taxas.API.Servicos;

namespace Taxas.API
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=taxas.db");
        }

        public DbSet<TaxaCondominio> Taxas { get; set; }
    }
}