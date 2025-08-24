using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;

namespace RedisExchangeAPI.Web.Controllers
{
    //burada redis service i kullanmak istiyorsak buranın contructorına DI olarak geçmek lazım
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
        }
        public IActionResult Index()
        {
            var db = _redisService.GetDb(0);
            db.StringSet("name", "Meryem Dinç");
            db.StringSet("visitors", 100);


            return View();
        }
    }
}
