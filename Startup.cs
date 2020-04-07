using Beam_intern.Scooter.Repository;
using Beam_intern.Scooter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Beam_intern
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
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            services.AddControllers();
            
            // For connection to postgres DB (with PostGis)
            services.AddDbContext<ScooterDbContext>(options => options.UseNpgsql("Postgres"));

            // Adding objects for dependency injection
            services.AddScoped<IScooterRepository, ScooterRepository>();
            services.AddScoped<IScooterService, ScooterService>();
            
            // For Swagger API documentation
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v0", new OpenApiInfo {Title = "Scooter Location API", Version = "v0"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v0/swagger.json", "Beam Intern Engineering Test API"));

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
