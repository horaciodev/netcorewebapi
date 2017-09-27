using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SampleAPI.Models;
using SampleAPI.Repositories;
using IdentityServer4.AccessTokenValidation;


namespace SampleAPI
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
            var catalogSqlConnStr = Configuration.GetConnectionString("ProductCatalogDB");

            var allowedCorsOrigin = Configuration["Cors:AllowedOrigin"];

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy=> 
                {
                policy.WithOrigins(allowedCorsOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
                });
            }
            );
            services.AddDbContext<TodoContext>(opt=> opt.UseInMemoryDatabase());
            // Add framework services.
            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IProductRepository, ProductRepository>(svcProvider =>{
                return new ProductRepository(catalogSqlConnStr);
            });
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>(svcProvider =>{
               return new ProductCategoryRepository(catalogSqlConnStr);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("default");

            var authority = Configuration["OAuth2:Authority"];

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions{
                Authority = authority,
                AllowedScopes = { "sampleAPI" },
                RequireHttpsMetadata = false,
            });

            app.UseMvc();
        }
    }
}
