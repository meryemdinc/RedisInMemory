using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
            _distributedCache.SetString("ad", "Fatih", options);
            await _distributedCache.SetStringAsync("soyad", "Cakiroglu");//await bu satırdaki işlem bitene kadar alt satıra geçilmemesini sağlar.
            return View();
        }
        public IActionResult Show()
        {
            string ad = _distributedCache.GetString("ad");
            ViewBag.ad = ad;
            return View();
        }
        public IActionResult Remove()
        {
            _distributedCache.Remove("ad");
            return View();
        }

    }
}