using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Hubs;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.Services.Concretes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.Cookie.Name = "UserIdentity";
                o.LoginPath = "/user/login";
            });


            builder.Services
               .AddDbContext<ECommerceDBContext>(ob =>
               {
                   ob.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
               })
               .AddScoped<IVerificationService, VerificationService>()
               .AddScoped<IUserService, UserService>()
               .AddScoped<IEmailService, EmailService>()
               .AddScoped<IFileService, FileService>()
               .AddScoped<INotificationService, NotificationService>()
               .AddScoped<IOrderService, OrderService>()
               .AddScoped<IUserActivationService, UserActivationService>()
               .AddSingleton<IAlertMessageService, AlertMessageService>()
               .AddHttpContextAccessor()
               .AddHttpClient();


            builder.Services
          .AddSignalR();

            var app = builder.Build();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.MapHub<AlertMessageHub>("/alert-hub"); //web-socket endpoint 
            app.Run();
        }
    }
}