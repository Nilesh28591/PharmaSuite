using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Data
{
    public class PharmaSuiteDBContext:DbContext
    {
        public PharmaSuiteDBContext(DbContextOptions<PharmaSuiteDBContext> options):base(options)
        {

    }
}
}
