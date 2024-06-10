using AspNetCoreWebAPIClientProject.Models; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AspNetCoreWebAPIClientProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:5260/api/Products\r\n"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Product product = new Product();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"http://localhost:5260/api/Products/{id}\r\n"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            return View(product);
        }

        // POST: ProductController/Create
        public async Task<IActionResult> Create([Bind("ProductId, ProductName, Price")]Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json,Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsync("http://localhost:5260/api/Products\r\n", content))
                {
                    if(response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                }
            }
            return View(product);   
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product product = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"http://localhost:5260/api/Products/{id}\r\n"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(ApiResponse);
                }
            }

            return View(product);
        }

        // PUT: ProductController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
           
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                using (var response = await client.PutAsync($"http://localhost:5260/api/Products/Edit/{id}", content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = null;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"http://localhost:5260/api/Products/{id}\r\n"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(ApiResponse);
                }
            }

            return View(product);
        }

        // DELETE: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
                using (var client = new HttpClient())
                {
                    using (var respone = await client.DeleteAsync($"http://localhost:5260/api/Products/Delete/{id}"))
                    {
                        if (!respone.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            return RedirectToAction("Index");
        }
    }
}
