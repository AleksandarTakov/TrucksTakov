using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrucksTakov.Data;
using TrucksTakov.Domain;
using TrucksTakov.Infrastructure;
using static TrucksTakov.Abstraction.IManufacturerService;
using TrucksTakov.Services;
using TrucksTakov.Abstraction;
using System.Text;

namespace TrucksTakov
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //  services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(
            //      Configuration.GetConnectionString("DefaultConnection")));
            // services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //     .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies()
            .UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<ITruckService, TruckService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IStatisticsService, StatisticsService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 5;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredUniqueChars = 0;
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            {
                string[] frazi = { "Камионът е отличен.", "Това е страхотен сайт.", "Давам 10/10, невероятно.", "Чудесен дизайн,отлични фънкции." };
                string[] sluchki = { "Харесаха ми филтрите,с тях лесно намерих желания камион.", "Направен е чудесно,всеки може да си поръча!.", "Много по-добър от останалите сайтове за камиони.", "Не мога да повярвам,че намерих толкова практичен сайт!.", "Опитайте и вие.Невероятно!." };
                string[] ime1 = { "Симона", "Елена", "Стела", "Наталия", "Анджелина" };
                string[] ime2 = { "Иванова", "Петрова", "Кирова" };
                string[] grad = { "София", "Перник", "Варна", "Русе", "Бургас" };

                Random r = new Random();

                StringBuilder asd = new StringBuilder();
                asd.Append($"{frazi[r.Next(0, frazi.Length)]} {sluchki[r.Next(0, sluchki.Length)]} - {ime1[r.Next(0, ime1.Length)]}, {grad[r.Next(0, grad.Length)]}");

                Console.WriteLine(asd.ToString());
            }
        }
    }
}
