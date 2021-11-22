using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using serverApp.Data;
using serverApp.Helpers;
using serverApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp
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
            services.AddCors();
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(name: "Default")));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(opt => opt
            .WithOrigins(new[] { "http://localhost:3000" })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
             );

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
