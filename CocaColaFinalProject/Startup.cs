using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CM.Data;
using CM.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using CM.Web.Infrastructure;
using CM.Data.DatabaseSeeder;
using CM.Data.JsonManager;
using CM.Services.Contracts;
using CM.Services;
using CM.Services.Common;

namespace CocaColaFinalProject
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
           
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddDbContext<CMContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<AppUser>()
                .AddRoles<AppRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<CMContext>();

            //register services here :
            services.AddDomainServices();
            services.AddScoped<IJsonManager, JsonManager>();
            //services.AddScoped<ICocktailServices, CocktailServices>();
            //services.AddScoped<IBarServices, BarServices>();
            //services.AddScoped<IAppUserServices, AppUserServices>();
            //services.AddScoped<IIngredientServices, IngredientServices>();
            //services.AddScoped<IReviewServices, ReviewServices>();
            //services.AddScoped<ISearchServices, SearchServices>();
            //services.AddScoped<IFileUploadService, FileUploadService>();
            //services.AddScoped<IStreamWriterServices, StreamWriterServices>();
            //services.AddScoped<INotificationManager, NotificationManager>();
            //services.AddScoped<INotificationServices, NotificationServices>();
            //services.AddScoped<IRecipeServices, RecipeServices>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Kendo
            services.AddKendo();
            //Toaster
            services.AddMvc().AddNToastNotifyToastr();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UpdateDatabase();
            app.SeedDatabaseCocktails();
            app.SeedDatabaseBars();
            app.SeedDatabaseAddresses();
            app.SeedDatabaseDistribution();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/error/{}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            //za tostera
            app.UseNToastNotify();
            app.UseMvc(routes =>
            {

                // areas routing here :
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
