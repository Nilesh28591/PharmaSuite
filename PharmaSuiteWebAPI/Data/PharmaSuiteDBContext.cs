using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Data
{
    public class PharmaSuiteDBContext:DbContext
    {
        public PharmaSuiteDBContext(DbContextOptions<PharmaSuiteDBContext> options):base(options)
        {
            
        }
        public DbSet<Purchase> purchase { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<PurchaseItem> purchaseItem { get; set; }
        public DbSet<Supplier> supplier { get; set; }
        public DbSet<Medicine_Management> Medicine_Managements { get; set; }
        public DbSet<Category> Medicine_categories { get; set; }
        public DbSet<Manifacturer_Medicine> Medicine_Manifacturer { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

    }
}
