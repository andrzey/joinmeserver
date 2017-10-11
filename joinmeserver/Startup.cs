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
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = "mongodb://localhost:27017"; //Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = "JoinmeDb"; //Configuration.GetSection("MongoConnection:Database").Value;
            });
            services.AddTransient<IHappeningRepository, HappeningRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
