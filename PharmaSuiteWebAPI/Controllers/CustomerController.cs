using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.DTO;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepo repo;
        private readonly PharmaSuiteDBContext db;

        public CustomerController(ICustomerRepo repo, PharmaSuiteDBContext db)
        {
            this.repo = repo;
            this.db = db;
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer(CustomerCreateDTO dto)
        {
            var customer = new Customer()
            {
                Name = dto.Name,
                Mobile = dto.Mobile,
                Address = dto.Address,
                Email = dto.Email,           // Included Email
                IsOnline = dto.IsOnline,     // Included IsOnline
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now
            };

            await repo.AddCustomerAsync(customer);
            return Ok(new { message = "Customer added successfully" });
        }

        [HttpGet]
        [Route("FetchCustomers")]
        public async Task<IActionResult> FetchCustomers()
        {
            var data = await repo.GetAllCustomers();
            var result = data.Select(c => new CustomerViewDTO
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Mobile = c.Mobile,
                Address = c.Address,
                Email = c.Email,           // Included Email
                IsOnline = c.IsOnline,     // Included IsOnline
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                UpdatedAt = c.UpdatedAt,
                UpdatedBy = c.UpdatedBy
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var c = await repo.GetCustomerByIdAsync(id);
            if (c == null)
                return NotFound(new { message = $"Customer with ID {id} not found" });

            var dto = new CustomerViewDTO()
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Mobile = c.Mobile,
                Address = c.Address,
                Email = c.Email,           // Included Email
                IsOnline = c.IsOnline,     // Included IsOnline
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                UpdatedAt = c.UpdatedAt,
                UpdatedBy = c.UpdatedBy
            };

            return Ok(dto);
        }

        [HttpPut]
        [Route("EditCustomer/{id}")]
        public async Task<IActionResult> EditCustomer(int id, CustomerUpdateDTO dto)
        {
            var c = await repo.GetCustomerByIdAsync(id);
            if (c == null)
                return NotFound(new { message = $"Customer with ID {id} not found" });

            c.Name = dto.Name;
            c.Mobile = dto.Mobile;
            c.Address = dto.Address;
            c.Email = dto.Email;           // Included Email
            c.IsOnline = dto.IsOnline;     // Included IsOnline
            c.UpdatedBy = dto.UpdatedBy;
            c.UpdatedAt = DateTime.Now;

            await repo.UpdateCustomerAsync(c);
            return Ok(new { message = "Customer updated successfully" });
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var c = await repo.GetCustomerByIdAsync(id);
            if (c == null)
                return NotFound(new { message = $"Customer with ID {id} not found" });

            await repo.DeleteCustomerAsync(id);
            return Ok(new { message = "Customer deleted successfully" });
        }

        //[HttpGet]
        //[Route("FetchSales")]
        //public IActionResult sales()
        //{
        //    var data = db.Sales.Include(x => x.c).Include(x => x.SaleItems).ThenInclude(x => x.Medicine).
        //        SelectMany(x => x.SaleItems.Select(y => new PurchaseHistoryDTO()
        //        {
        //            CustomerName = x.CustomerName,
        //            Mobile = x.Customer.Mobile,
        //            MedicineName = y.Medicine.Name,
        //            TotalAmount = x.TotalAmount,
        //            Discount = y.Discount,
        //            Quantity = y.Quantity

        //        })).ToList();
        //    return Ok(data);
        //}
    }
}
