using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaSuiteMVC.Models;
using System.Text;

namespace PharmaSuiteMVC.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly HttpClient _client;

        public ExpenseController()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (msg, cert, chain, error) => true;
            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7259/api/Expenses/")
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("all");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ExpenseDto>>(json);
            return View(data);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(ExpenseDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7259/api/Expenses/add", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            // Log status and message
            var error = await response.Content.ReadAsStringAsync();
            ViewBag.Error = $"Failed to add expense. Status: {(int)response.StatusCode}. Error: {error}";
            return View(dto);
        }



        public IActionResult Profit() => View();

        [HttpPost]
        public async Task<IActionResult> Profit(decimal totalSales, decimal totalCostOfGoods)
        {
            var response = await _client.GetAsync($"profit?totalSales={totalSales}&totalCostOfGoods={totalCostOfGoods}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to calculate profit";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProfitDto>(json);
            return View(result);
        }
    }
}