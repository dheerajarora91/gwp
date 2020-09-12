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
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Country GWP API", 
                    Version = "v1",
                    Description = "An API to perform country specific GWP operations",
                });
            });

            services.AddCors();

            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseResponseCaching();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Country GWP API");
            });

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
