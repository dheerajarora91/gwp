using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryGWP.DataAccess.Layer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CountryGWP.DataAccess.Layer.Repositories.Interfaces;
using CountryGWP.Business.Layer.Services.Interfaces;
using CountryGWP.DataAccess.Layer.Repositories;
using CountryGWP.Business.Layer.Services;

namespace CountryGWP
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
            services.AddControllers();

            var connectionString = Configuration["ConnectionStrings:CountryGwpDbConnectionString"];

            services.AddDbContext<CountryGwpDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ICountryGwpRepository, CountryGwpRepository>();
            services.AddScoped<ICountryGwpService, CountryGwpService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CountryGwpDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
