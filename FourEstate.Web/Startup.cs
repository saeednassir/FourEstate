using FourEstate.Data;
using FourEstate.Data.Models;
using FourEstate.infrastructure.Services.ContractSS;
using FourEstate.infrastructure.Services.Dashbords;
using FourEstate.Infrastructure.AutoMapper;
using FourEstate.Infrastructure.Middlewares;
using FourEstate.Infrastructure.Services;
using FourEstate.Infrastructure.Services.Advertisements;
using FourEstate.Infrastructure.Services.Categories;
using FourEstate.Infrastructure.Services.Customers;
using FourEstate.Infrastructure.Services.LocationsService;
using FourEstate.Infrastructure.Services.REAlEstate;
using FourEstate.Infrastructure.Services.Users;
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

namespace FourEstate.Web
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
            services.AddDbContext<FourEstateDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 6;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<FourEstateDbContext>()
                        .AddDefaultTokenProviders().AddDefaultUI();

            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IRealEstateService, RealEstateService>();
            services.AddScoped<IDashboardService, DashboardService>();





            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseExceptionHandler(opts => opts.UseMiddleware<ExceptionHandler>());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
