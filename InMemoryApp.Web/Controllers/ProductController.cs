using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memorycache)
        {
            _memoryCache = memorycache;
        }



        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration=DateTime.Now.AddMinutes(1);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);

                _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);
            }

            return View();
        }

        public IActionResult Show()
        {
            /*  _memoryCache.GetOrCreate<string>("zaman", entry =>
              {

                  return DateTime.Now.ToString();
              });*/
            _memoryCache.TryGetValue("zaman", out string zamancache);
            ViewBag.zaman = zamancache;
            return View();
        }
    }
}