﻿using Microsoft.AspNetCore.Mvc;
using PharmaSuiteMVC.Models;


using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace PharmaSuiteMVC.Controllers
{
    public class MedicineController : Controller
    {
        HttpClient client;
        public MedicineController() 
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (SslPolicyErrors, chain, cert, sender) => { return true; };
            client = new HttpClient(handler);
        }
        public IActionResult Report()
        {
            List<PurchaseItemDto> meds = new List<PurchaseItemDto>();
            string url = "https://localhost:7259/api/Medicine/StockAlert";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<int>(json);
                ViewBag.Count = obj;
            }

            List<PurchaseItemDto> exp = new List<PurchaseItemDto>();
            string url2 = "https://localhost:7259/api/Medicine/ExpAlert";
            HttpResponseMessage response2 = client.GetAsync(url2).Result;
            if (response2.IsSuccessStatusCode)
            {
                var json2 = response2.Content.ReadAsStringAsync().Result;
                var obj2 = JsonConvert.DeserializeObject<int>(json2);
                ViewBag.Exp = obj2;
            }

            List<PurchaseItemDto> exp_alert = new List<PurchaseItemDto>();
            string url3 = "https://localhost:7259/api/Medicine/PriorExpAlert";
            HttpResponseMessage response3 = client.GetAsync(url3).Result;
            if (response3.IsSuccessStatusCode)
            {
                var json2 = response3.Content.ReadAsStringAsync().Result;
                var obj2 = JsonConvert.DeserializeObject<int>(json2);
                ViewBag.ExpAlert = obj2;
            }

            List<PurchaseItemDto> Stock_alert_table = new List<PurchaseItemDto>();
            string url4 = "https://localhost:7259/api/Medicine/StockAlertTable/";
            HttpResponseMessage response4 = client.GetAsync(url4).Result;
            if (response4.IsSuccessStatusCode)
            {
                var json2 = response4.Content.ReadAsStringAsync().Result;
                var obj2 = JsonConvert.DeserializeObject<List<PurchaseItemDto>>(json2);
                ViewBag.StockAlertTable = obj2;
            }

            List<PurchaseItemDto> exp_alert_table = new List<PurchaseItemDto>();
            string url5 = "https://localhost:7259/api/Medicine/ExpAlertTable/";
            HttpResponseMessage response5 = client.GetAsync(url5).Result;
            if (response5.IsSuccessStatusCode)
            {
                var json2 = response5.Content.ReadAsStringAsync().Result;
                var obj2 = JsonConvert.DeserializeObject<List<PurchaseItemDto>>(json2);
                ViewBag.expAlertTable = obj2;
            }

            List<PurchaseItemDto> prior_exp_alert_table = new List<PurchaseItemDto>();
            string url6 = "https://localhost:7259/api/Medicine/PriorExpAlertTable/";
            HttpResponseMessage response6 = client.GetAsync(url6).Result;
            if (response6.IsSuccessStatusCode)
            {
                var json8 = response6.Content.ReadAsStringAsync().Result;
                var obj8 = JsonConvert.DeserializeObject<List<PurchaseItemDto>>(json8);
                ViewBag.PriorExpAlertTable = obj8;
            }


            return View();
        }

        public IActionResult Index()
        {
            List<Medicine_Management> meds = new List<Medicine_Management>();
            string url = "https://localhost:7290/api/Medicine_Management/Fetch/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Medicine_Management>>(json);
                if (obj != null)
                {
                    meds = obj;
                }
            }
            return View(meds);
        }


        public IActionResult Add_Medicine()
        {
            List<Category_Dto> cat = new List<Category_Dto>();
            string url = "https://localhost:7290/api/Medicine_Management/FetchCat/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var stringfy = response.Content.ReadAsStringAsync().Result;
                var json = JsonConvert.DeserializeObject<List<Category_Dto>>(stringfy);
                if (json != null)
                {
                    cat = json;
                }
                ViewBag.Cat = new SelectList(cat, "categoryName", "categoryName");
            }


            List<Manifacture_Dto> mfg = new List<Manifacture_Dto>();
            string url2 = "https://localhost:7290/api/Medicine_Management/FetchMfg/";
            HttpResponseMessage response2 = client.GetAsync(url2).Result;
            if (response2.IsSuccessStatusCode)
            {
                var stringfy2 = response2.Content.ReadAsStringAsync().Result;
                var json2 = JsonConvert.DeserializeObject<List<Manifacture_Dto>>(stringfy2);
                if (json2 != null)
                {
                    mfg = json2;
                }
                ViewBag.Mfg = new SelectList(mfg, "mName", "mName");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Add_Medicine(Medicine_Management meds)
        {
            string url = "https://localhost:7290/api/Medicine_Management/Add/";
            var json = JsonConvert.SerializeObject(meds);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }


        public IActionResult Delete_Medicine(int id)
        {
            Medicine_Management meds = new Medicine_Management();
            string url = "https://localhost:7290/api/Medicine_Management/Delete/";
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit_Medicine(int id)
        {
            List<Category_Dto> cat = new List<Category_Dto>();
            string url = "https://localhost:7290/api/Medicine_Management/FetchCat/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var stringfy = response.Content.ReadAsStringAsync().Result;
                var json = JsonConvert.DeserializeObject<List<Category_Dto>>(stringfy);
                if (json != null)
                {
                    cat = json;
                }
                ViewBag.Cat = new SelectList(cat, "categoryName", "categoryName");
            }


            List<Manifacture_Dto> mfg = new List<Manifacture_Dto>();
            string url2 = "https://localhost:7290/api/Medicine_Management/FetchMfg/";
            HttpResponseMessage response2 = client.GetAsync(url2).Result;
            if (response2.IsSuccessStatusCode)
            {
                var stringfy2 = response2.Content.ReadAsStringAsync().Result;
                var json2 = JsonConvert.DeserializeObject<List<Manifacture_Dto>>(stringfy2);
                if (json2 != null)
                {
                    mfg = json2;
                }
                ViewBag.Mfg = new SelectList(mfg, "mName", "mName");

            }
            Medicine_Management meds = new Medicine_Management();
            string url3 = "https://localhost:7290/api/Medicine_Management/Edit/";
            HttpResponseMessage response3 = client.GetAsync(url3 + id).Result;
            if (response3.IsSuccessStatusCode)
            {
                var json3 = response3.Content.ReadAsStringAsync().Result;
                var obj3 = JsonConvert.DeserializeObject<Medicine_Management>(json3);
                if (obj3 != null)
                {
                    meds = obj3;
                }
            }
            return View(meds);
        }

        [HttpPost]
        public IActionResult Edit_Medicine(Medicine_Management meds)
        {
            string url = "https://localhost:7290/api/Medicine_Management/PostEdit/";
            var json = JsonConvert.SerializeObject(meds);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Add_Cat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Cat(CategoryDto_Add dto)
        {
            string url = "https://localhost:7290/api/Medicine_Management/AddCat/";
            var json = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json("");
            }
            //{
            //    string error = response.Content.ReadAsStringAsync().Result;
            //    Console.WriteLine("Error: " + error);
            //}
            return View();
        }

        public IActionResult Add_Mfg()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add_Mfg(Manifacture_Dto_add add)
        {
            string url = "https://localhost:7290/api/Medicine_Management/AddMfg/";
            var json = JsonConvert.SerializeObject(add);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
