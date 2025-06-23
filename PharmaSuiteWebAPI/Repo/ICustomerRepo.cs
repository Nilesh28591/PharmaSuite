using PharmaSuiteWebAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaSuiteWebAPI.Repo
{
    public interface ICustomerRepo
    {
        Task AddCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }

}
