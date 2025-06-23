using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.DTO;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepo repo;
        public CustomerController(ICustomerRepo repo)
        {
            this.repo = repo;
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
    }
}
