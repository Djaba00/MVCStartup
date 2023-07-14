using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCStartup.Middlewares;
using MVCStartup.Models.DB;
using MVCStartup.Models.DB.Entities;
using MVCStartup.Models.DB.Repositories;


namespace MVCStartup
{
    public class Startup
    {
        static IWebHostEnvironment _env;
        IConfiguration _cfg;

        public Startup(IWebHostEnvironment environmentSet, IConfiguration configurationSet)
        {
            _env = environmentSet;
            _cfg = configurationSet;
        }

        // Вызывается стредой ASP.NET.
        // Используется для подключения сервисов приложения.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            if(_env.IsDevelopment())
            {
                services.AddSingleton<IRequestRepository, RequestRepository>();
            }

            string connection = _cfg.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
            services.AddControllersWithViews();
        }

        // Вызывается стредой ASP.NET.
        // Используется для настройки конвейера запросов.
        public static void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // обрабатываем ошибки HTTP
            app.UseStatusCodePages();

            // компонент, отвечающий за маршрутизацию
            app.UseRouting();

            // поддержка статических файлов
            app.UseStaticFiles();
            
            // подключение логгирования с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
