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

                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            return View();
        }

        public IActionResult Show()
        {
          /*  _memoryCache.GetOrCreate<string>("zaman", entry =>
            {
               
                return DateTime.Now.ToString();
            });*/
            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}