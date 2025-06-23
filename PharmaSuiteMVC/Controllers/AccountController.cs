using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaSuiteMVC.Models;
using System.Text;

namespace PharmaSuiteMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly string? _apiBaseUrl;
        HttpClient client;
        public AccountController(IConfiguration configuration)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (SslPolicyErrors, chain, cert, sender) => { return true; };
            client = new HttpClient(handler);
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"];
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_LoginPartial");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_LoginPartial", model);

            var payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}/api/User/Login", payload);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);
                HttpContext.Session.SetString("UserRole", (string)data.role);
                return Json(new { success = true }); // ✅ AJAX Response
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return PartialView("_LoginPartial", model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_RegisterPartial");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_RegisterPartial", model);

            var payload = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}/api/User/Register", payload);
            if (response.IsSuccessStatusCode) return Json(new { success = true }); // ✅ AJAX Response

            ModelState.AddModelError(string.Empty, "Registration failed");
            return PartialView("_RegisterPartial", model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserRole");
            return RedirectToAction("Login");
        }
    }
}
