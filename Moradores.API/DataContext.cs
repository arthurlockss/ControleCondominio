using Microsoft.EntityFrameworkCore;
using Moradores.API.Servicos; 

namespace Moradores.API
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=moradores.db");
        }

        public DbSet<Morador> Moradores { get; set; }
    }
}