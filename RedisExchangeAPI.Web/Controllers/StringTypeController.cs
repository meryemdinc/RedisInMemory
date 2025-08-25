using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;

namespace RedisExchangeAPI.Web.Controllers
{
    //burada redis service i kullanmak istiyorsak buranın contructorına DI olarak geçmek lazım
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {

            db.StringSet("name", "Meryem Dinç");
            db.StringSet("visitors", 100);


            return View();
        }
        public IActionResult Show()
        {
            var value= db.StringGet("name");
            if (value.HasValue)
            {
                ViewBag.value=value.ToString();
            }
            db.StringIncrement("visitors", 1);
             var value2 = db.StringDecrementAsync("visitors", 10).Result;

                ViewBag.value2 = value2.ToString();
         
            var value3=db.StringGetRange("name", 0, 3);
            ViewBag.value3 = value3.ToString();

            var value4=db.StringLength("name");
            ViewBag.value4 = value4.ToString(); 

            return View();
        }
    }
}
