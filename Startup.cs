using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Groupproj.Models;
using Microsoft.EntityFrameworkCore;
//using Groupproj.Data;
namespace Groupproj
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
            services.AddSingleton<IAuthorizationService, >();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthorization(options =>
            {
                services.AddAuthorization(options =>
          options.AddPolicy("User",
          policy => policy.RequireClaim("IsUser", "CanViewTimeTable")
          options.AddPolicy("Admin",
          policy => policy.RequireClaim("IsAdmin", "CanEditTimeTable", "CanViewTimeTable");
            });

          
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddDbContext<DbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("unidb")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
