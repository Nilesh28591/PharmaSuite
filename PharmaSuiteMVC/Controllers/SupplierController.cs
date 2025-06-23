using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaSuiteMVC.Filters;
using PharmaSuiteMVC.Models;

namespace PharmaSuiteMVC.Controllers
{
    [MyException]
    public class SupplierController : Controller
    {
        HttpClient client;
        public SupplierController()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (SslPolicyErrors, chain, cert, sender) => { return true; };
            client = new HttpClient(handler);
        }

        public IActionResult Index()
        {

            //List<SupplierDTO> meds = new List<SupplierDTO>();
            //string url = "https://localhost:7259/api/Supplier/GetSup/";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var json = response.Content.ReadAsStringAsync().Result;
            //    var obj = JsonConvert.DeserializeObject<List<SupplierDTO>>(json);
            //    if (obj != null)
            //    {
            //        meds = obj;
            //    }
            //}
            //return View(meds);


            return View();

            // return Json(meds);
        }



        [HttpGet]
        public JsonResult GetSuppliers()
        {
            List<SupplierDTO> meds = new List<SupplierDTO>();
            string url = "https://localhost:7259/api/Supplier/GetSup/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<SupplierDTO>>(json);
                if (obj != null)
                {
                    meds = obj;
                }
            }
            return Json(meds);
        }






        public IActionResult AddSupView()
        {
            return View();
        }


        public IActionResult Exc()
        {
            int a = 10;
            int b = a/0;
            return View(b);
        }

        //[HttpPost]
        //public IActionResult AddSup([FromBody]SupplierDTO dto)
        //{
        //    string url = "https://localhost:7259/api/Supplier/AddSup/";
        //    var json = JsonConvert.SerializeObject(dto);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = client.PostAsync(url, content).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}


        [HttpPost]
        public JsonResult AddSup([FromBody] SupplierDTO dto)
        {
            string url = "https://localhost:7259/api/Supplier/AddSup/";
            var json = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "API call failed" });
        }



        public IActionResult DeleteSup(int id)
        {

            SupplierDTO meds = new SupplierDTO();
            string url = "https://localhost:7259/api/Supplier/DeleteSup/";
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditSupView(int id)
        {
            SupplierDTO meds = new SupplierDTO();
            string url = "https://localhost:7259/api/Supplier/GetSupById/";
            HttpResponseMessage response = await client.GetAsync(url + id);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<SupplierDTO>(json);
                if (obj != null)
                {
                    meds = obj;
                }
            }
            return Json(meds);


            //return View();
        }


        //[HttpPost]
        //public IActionResult EditSup(SupplierDTO dto)
        //{
        //    string url = "https://localhost:7259/api/Supplier/EditSup/";
        //    var json = JsonConvert.SerializeObject(dto);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = client.PostAsync(url, content).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public async Task<IActionResult> EditSup(SupplierDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //        return View(dto);

        //    string url = "https://localhost:7259/api/Supplier/EditSup/";
        //    var json = JsonConvert.SerializeObject(dto);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await client.PutAsync(url, content);
        //    var responseContent = await response.Content.ReadAsStringAsync(); // Log this

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ApiError = responseContent; // Optional: show in the view
        //    return View(dto);
        //}



        [HttpPost]
        public async Task<IActionResult> EditSup(SupplierDTO dto)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState });

            string url = "https://localhost:7259/api/Supplier/EditSup/";
            var json = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Json(new { success = true });

            return Json(new { success = false, message = responseContent });
        }








    }
}
