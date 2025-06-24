using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaSuiteMVC.Models;

namespace PharmaSuiteMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client;
        private readonly string apiUrl = "https://localhost:7259/api/User";

        public UserController()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            client = new HttpClient(handler);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var payload = new
            {
                Username = user.Username,
                Password = user.Password
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync($"{apiUrl}/Login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseStr = response.Content.ReadAsStringAsync().Result;
                dynamic result = JsonConvert.DeserializeObject<dynamic>(responseStr);

                string role = result.role.ToString();
                string userId = result.userId.ToString();

                TempData["Role"] = role;
                TempData["UserId"] = userId;

                // 🔁 Role-based redirection
                if (role == "Admin")
                    return RedirectToAction("Index", "Medicine");
                else if (role == "Customer")
                    return RedirectToAction("Index", "Purchase");
                else
                    return RedirectToAction("Index", "UserDashboard");
            }

            ViewBag.Message = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var payload = new
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync($"{apiUrl}/Register", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Username already taken.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
