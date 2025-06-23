using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaSuiteMVC.Models;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;

namespace PharmaSuiteMVC.Controllers
{
    public class AjaxCustomerController : Controller
    {
        private readonly HttpClient client;
        private readonly string apiUrl = "https://localhost:7259/api/Customer";

        public AjaxCustomerController()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => true;
            client = new HttpClient(handler);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            HttpResponseMessage response = client.GetAsync($"{apiUrl}/FetchCustomers").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                return Json(customers);
            }
            return Json(null);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpResponseMessage response = client.GetAsync($"{apiUrl}/GetCustomerById/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var customer = JsonConvert.DeserializeObject<Customer>(json);
                return Json(customer);
            }
            return Json(null);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Customer customer)
        {
            // API expects CustomerCreateDTO - no CustomerId, CreatedBy passed here
            var dto = new
            {
                Name = customer.Name,
                Mobile = customer.Mobile,
                Address = customer.Address,
                CreatedBy = "admin"  // you can change as per your auth system
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"{apiUrl}/AddCustomer", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Customer added successfully" });
            }
            return Json(new { success = false, message = "Failed to add customer" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Customer customer)
        {
            // API expects CustomerUpdateDTO without id in body, but id in route
            var dto = new
            {
                Name = customer.Name,
                Mobile = customer.Mobile,
                Address = customer.Address,
                UpdatedBy = "admin" // change if you have user info
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"{apiUrl}/EditCustomer/{customer.CustomerId}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Customer updated successfully" });
            }
            return Json(new { success = false, message = "Failed to update customer" });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{apiUrl}/DeleteCustomer/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Customer deleted successfully" });
            }
            return Json(new { success = false, message = "Failed to delete customer" });
        }
    }
}
