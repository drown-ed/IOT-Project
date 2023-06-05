using Microsoft.EntityFrameworkCore;
using PortfolioWeb.Models;

namespace PortfolioWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<PortfolioModel> Portfolios { get; set; }
    }
}
