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
        public DbSet<PurchaseItem> purchaseItem { get; set; }
        public DbSet<Supplier> supplier { get; set; }
        public DbSet<Medicine> medicine { get; set; }   

    }
}
