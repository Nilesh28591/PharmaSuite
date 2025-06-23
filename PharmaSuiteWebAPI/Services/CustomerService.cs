using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;
using System.Collections.Generic;
using System.Linq;

namespace PharmaSuiteWebAPI.Services
{
    public class CustomerService : ICustomerRepo
    {
        private readonly PharmaSuiteDBContext db;

        public CustomerService(PharmaSuiteDBContext db)
        {
            this.db = db;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await db.Customers.AddAsync(customer);
            await db.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await db.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await db.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            db.Customers.Update(customer);
            await db.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
            }
        }
    }
}
