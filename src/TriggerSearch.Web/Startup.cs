using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TriggerSearch.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TriggerSearch.Core;
using TriggerSearch.Service;
using TriggerSearch.Data.Models;
using TriggerSearch.Contract.Services;
using TriggerSearch.Web.Seeds;
using TriggerSearch.Search.ElasticSearch;

namespace TriggerSearch.Web
{
    public class Startup
    {
        private readonly string _assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        private readonly string _dbConnnectionName = "DefaultConnection";
        private readonly string _dbPostgre = "PostgreConnection";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<PermissionContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString(_dbConnnectionName),
            //    option => option.MigrationsAssembly(_assemblyName)));

            services.AddDbContext<PermissionContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString(_dbPostgre),
                option => option.MigrationsAssembly(_assemblyName)));

            
            services.AddUnitOfWork<PermissionContext>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddMvc();



            services.AddElasticSearchClient(Configuration.GetElasticConfiguration());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PermissionContext context)
        {
            context.Database.Migrate();
            Seeding seeding = new Seeding(context);
            seeding.Init();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
