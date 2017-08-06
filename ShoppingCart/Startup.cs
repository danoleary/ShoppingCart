using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.Services;

namespace ShoppingCart
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ShoppingCartContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();
            services.AddTransient<ItemRepository, ItemRepository>();
            services.AddTransient<UserRepository, UserRepository>();
            services.AddTransient<BasketRepository, BasketRepository>();
            services.AddTransient<OrderRepository, OrderRepository>();
            services.AddTransient<ItemImportService, ItemImportService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.SeedDb();
        }
    }

    public static class DatabaseSeeder
    {
        public static void SeedDb(this IApplicationBuilder app)
        {
            var itemImportService = app.ApplicationServices.GetRequiredService<ItemImportService>();
            itemImportService.ImportItems().Wait();
            var userRepo = app.ApplicationServices.GetRequiredService<UserRepository>();
            userRepo.Add().Wait();
        }
    }

    
}
