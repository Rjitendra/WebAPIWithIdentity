using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Contexts;

namespace WebAPIWithAsp.NetIdentityModel
{
    public class Startup
    {
        private readonly string ConnectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // default connection string from appsettings.json
            ConnectionString = Configuration.GetConnectionString("EmployeeDBConnection");
        }
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddDbContextPool<AppDbContext>(
           //options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDBConnection")));

            // fetch connection string from appsettings.json var connectionString = this.Configuration.GetConnectionString("EmployeeDBConnection");
            // configure DB context for injection
           
            var assembly = typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<AppDbContext>(builder => builder.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(assembly)));


            services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
