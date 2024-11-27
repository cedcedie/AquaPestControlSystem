using Microsoft.EntityFrameworkCore;
using AquaPestControlSystem.Models;

namespace AquaPestControlSystem.DAL
{
    public class ProprieterCustomerDBContext : DbContext
    {
        public ProprieterCustomerDBContext(DbContextOptions<ProprieterCustomerDBContext> options)
            : base(options)
        {
        }

        public DbSet<ProprietorCustomer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=CEDRICJAMES\\SQLEXPRESS;Database=AquaPestDB;Trusted_Connection=True;");
            }
        }
    }
}
