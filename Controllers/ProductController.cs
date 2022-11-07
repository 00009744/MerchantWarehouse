using MerchantWarehouse.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        string baseApiAddress = "http://ec2-44-211-170-56.compute-1.amazonaws.com/";
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<Product> Products = new List<Product>();
            using (var product = new HttpClient())
            {
                product.BaseAddress = new Uri(baseApiAddress);
                product.DefaultRequestHeaders.Clear();

                product.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await product.GetAsync("api/Product");

                if (res.IsSuccessStatusCode)
                {
                    var prRes = res.Content.ReadAsStringAsync().Result;

                    Products = JsonConvert.DeserializeObject<List<Product>>(prRes);
                }
            }
            return View("Index", Products);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Product/
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiAddress);
                client.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(product);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await client.PostAsync("api/Product", httpContent);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        // GET: Product/Id
        public async Task<ActionResult> Edit(int ID)
        {
            Product Products = new Product();
            using (var product = new HttpClient())
            {
                product.BaseAddress = new Uri(baseApiAddress);
                product.DefaultRequestHeaders.Clear();

                product.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await product.GetAsync("api/Product/" + ID);

                if (res.IsSuccessStatusCode)
                {
                    var prRes = res.Content.ReadAsStringAsync().Result;

                    Products = JsonConvert.DeserializeObject<Product>(prRes);
                }
            }
            return View("Create", Products);
        }

        // PUT: Product/
        [HttpPost]
        public async Task<ActionResult> Edit(Product product)
        {
            using (var products = new HttpClient())
            {
                products.BaseAddress = new Uri(baseApiAddress);
                products.DefaultRequestHeaders.Clear();

                string json = JsonConvert.SerializeObject(product);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await products.PutAsync(products.BaseAddress + "api/Product", httpContent);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("Create", product);

            }
        }

        // GET: Product/Id
        public async Task<ActionResult> Details(int ID)
        {
            Product product = new Product();
            using (var prod = new HttpClient())
            {
                prod.BaseAddress = new Uri(baseApiAddress);
                prod.DefaultRequestHeaders.Clear();
                prod.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await prod.GetAsync("api/Product/" + ID);

                if (res.IsSuccessStatusCode)
                {
                    var prRes = res.Content.ReadAsStringAsync().Result;

                    product = JsonConvert.DeserializeObject<Product>(prRes);
                }
            }
            return View(product);
        }

        // DELETE Product/Id
        public async Task<ActionResult> Delete(int ID)
        {
            using (var products = new HttpClient())
            {
                products.BaseAddress = new Uri(baseApiAddress);
                products.DefaultRequestHeaders.Clear();

                products.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await products.DeleteAsync("api/Product/" + ID);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
    }
}