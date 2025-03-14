using Microsoft.EntityFrameworkCore;
using PoliziaMunicipaleApp.Models;

namespace PoliziaMunicipaleApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Trasgressore> Trasgressori { get; set; }
        public DbSet<Violazione> Violazioni { get; set; }
        public DbSet<Verbale> Verbali { get; set; }
    }
}