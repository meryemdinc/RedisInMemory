using RedisExchangeAPI.Web.Services;

namespace RedisExchangeAPI.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Servisleri ekle
            builder.Services.AddSingleton<RedisService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Redis bağlanma
            var redisService = app.Services.GetRequiredService<RedisService>();
            redisService.Connect();

            // Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
