using Microsoft.EntityFrameworkCore;
using AquaPestControlSystem.Models.DBEntities;

namespace AquaPestControlSystem.DAL
{
    public class ProprieterCustomerDBContext : DbContext
    {
        public ProprieterCustomerDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Technician> Technicians { get; set; }
    }
}
