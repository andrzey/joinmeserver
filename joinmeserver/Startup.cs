using joinmeserver.Models;
using joinmeserver.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace joinmeserver
{
    public class Startup
    {
        public static IConfiguration Config { get; private set; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Config.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Config.GetSection("MongoConnection:Database").Value;
            });

            services.AddScoped<HappeningRepository>();
            services.AddScoped<UserRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
