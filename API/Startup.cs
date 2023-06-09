using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Core;
using API.Model.Utils;
using API.Data.Repository;
using API.Data.Interfaces;
using API.Data.Dapper;
using API.Core.Managers;
using Microsoft.OpenApi;

namespace API
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
            services.AddTransient<IListingsManager, ListingsManager>();
            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<IDbManager, DbManager>();
            services.Configure<AppConfig>(Configuration.GetSection("ConnectionStrings"));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
         
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // initialise Dapper Fluent mappings
            API.Core.Registrations.Register();
        }
    }
}
