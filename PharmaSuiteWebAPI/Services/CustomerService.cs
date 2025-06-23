using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.DTO;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // --------- Additional helper methods to work with DTOs ---------

        public async Task AddCustomerAsync(CustomerCreateDTO dto)
        {
            var customer = new Customer()
            {
                Name = dto.Name,
                Mobile = dto.Mobile,
                Address = dto.Address,
                Email = dto.Email,           // added Email
                IsOnline = dto.IsOnline,     // added IsOnline
                CreatedBy = dto.CreatedBy,
                CreatedAt = System.DateTime.Now
            };
            await AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerUpdateDTO dto)
        {
            var customer = await GetCustomerByIdAsync(dto.CustomerId);
            if (customer == null)
                throw new System.Exception($"Customer with ID {dto.CustomerId} not found.");

            customer.Name = dto.Name;
            customer.Mobile = dto.Mobile;
            customer.Address = dto.Address;
            customer.Email = dto.Email;         // added Email
            customer.IsOnline = dto.IsOnline;   // added IsOnline
            customer.UpdatedBy = dto.UpdatedBy;
            customer.UpdatedAt = System.DateTime.Now;

            await UpdateCustomerAsync(customer);
        }

        public async Task<List<CustomerViewDTO>> GetAllCustomersDTOAsync()
        {
            var customers = await GetAllCustomers();
            var result = new List<CustomerViewDTO>();
            foreach (var c in customers)
            {
                result.Add(new CustomerViewDTO()
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Mobile = c.Mobile,
                    Address = c.Address,
                    Email = c.Email,             // added Email
                    IsOnline = c.IsOnline,       // added IsOnline
                    CreatedAt = c.CreatedAt,
                    CreatedBy = c.CreatedBy,
                    UpdatedAt = c.UpdatedAt,
                    UpdatedBy = c.UpdatedBy
                });
            }
            return result;
        }

        public async Task<CustomerViewDTO> GetCustomerByIdDTOAsync(int id)
        {
            var c = await GetCustomerByIdAsync(id);
            if (c == null)
                return null;

            return new CustomerViewDTO()
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Mobile = c.Mobile,
                Address = c.Address,
                Email = c.Email,               // added Email
                IsOnline = c.IsOnline,         // added IsOnline
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                UpdatedAt = c.UpdatedAt,
                UpdatedBy = c.UpdatedBy
            };
        }
    }
}
