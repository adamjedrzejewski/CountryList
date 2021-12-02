using CountryList.Countries;
using CountryList.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CountryList
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
            // Description does not mention how country data should be represented.
            // Storing it in a config file and generating path finding information from it
            // is suitable for data of small size.
            // For data of greater size, path finding information should be generated ahead of time
            // and loaded on program startup.
            var countriesConfig = JsonSerializer.Deserialize<CountriesConfiguration>(
                File.ReadAllText("countries.json")
            );
            var countriesGraph = CountriesGraph.CreateCountriesGraph(countriesConfig);
            var countriesPathFinder = CountryListService.CreateCountryListSerivce(countriesGraph, countriesConfig.StartingPoint);

            services.AddControllers();
            services.AddSingleton<ICountryListService, CountryListService>(
                _ => countriesPathFinder
            );
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
        }

    }
}
