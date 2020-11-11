using AutoMapper;
using FraktonProficiencyTest.Extensions;
using FraktonProficiencyTest.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FraktonProficiencyTest
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
            #region DbContext
            services.AddDbContext<DataContext>();
            #endregion

            #region Controllers
            services.AddControllers();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region JwtSettings
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            #endregion

            #region  Authentication
            services.AddAuthenticationConfig(jwtSettings);
            #endregion

            #region Swagger Configuration
            services.AddSwaggerConfig();
            #endregion

            #region Service Dependencies
            services.RegisterServiceDependencies();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #endregion

            #region UseAuthentication
            app.UseAuthenticationConfig();
            #endregion

            #region Routing
            app.UseRouting();
            #endregion

            #region Use Authorization
            app.UseAuthorization();
            #endregion

            #region Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion

            #region Use Swagger
            app.UseSwaggerConfig();
            #endregion
        }
    }
}
