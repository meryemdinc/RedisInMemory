using IDistibutedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace IDistibutedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }



        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            Product product = new Product
            {
                ID = 1,
                Name = "Kalem",
                Price = 10
            };
            string jsonProduct = JsonConvert.SerializeObject(product);
            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
            _distributedCache.Set("product:1", byteProduct, options);
            //   await   _distributedCache.SetStringAsync("product:1", jsonProduct, options);
            /*   _distributedCache.SetString("ad", "Fatih", options);
               await _distributedCache.SetStringAsync("soyad", "Cakiroglu"); //await bu satırdaki işlem bitene kadar alt satıra geçilmemesini sağlar.*/
            return View();
        }
        public IActionResult Show()
        {
            Byte[] byteProduct = _distributedCache.Get("product:1");
            string byteProductString = Encoding.UTF8.GetString(byteProduct);
            Product p = JsonConvert.DeserializeObject<Product>(byteProductString);
            ViewBag.product = p;

            //  string jsonProduct = _distributedCache.GetString("product:1");
            //  Product p=JsonConvert.DeserializeObject<Product>(jsonProduct);
            //  ViewBag.product = p;

            //  string ad = _distributedCache.GetString("ad");
            //ViewBag.ad = ad;
            return View();
        }
        public IActionResult Remove()
        {
            _distributedCache.Remove("ad");
            return View();
        }

        public IActionResult ImageCache()
        {
           DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(10)); 
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/download.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("resim", imageByte);
            return View();
        }
        public IActionResult ImageUrl()
        {
            byte[] resimByte = _distributedCache.Get("resim");  

            return File(resimByte, "image/jpeg"); // Burada resim türünü belirtmek önemlidir.

        }
        public IActionResult ImageRemove()
        {
            _distributedCache.Remove("resim"); // cache'ten resmi sil
            return RedirectToAction("Index"); // İstersen RedirectToAction ile başka bir sayfaya yönlendirebilirsin
        }
       

    }
}