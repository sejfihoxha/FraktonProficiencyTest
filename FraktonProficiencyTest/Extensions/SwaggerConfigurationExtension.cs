using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FraktonProficiencyTest.Extensions
{
    public static class SwaggerConfigurationExtension
    {
        public static void AddSwaggerConfig(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Frakton Proficiency Test API",
                    Version = "V1.0",
                    Description = "Frakton Proficiency Test API"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                   {
                                     new OpenApiSecurityScheme
                                     {
                                       Reference = new OpenApiReference
                                       {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                       }
                                      },
                                      new string[] { }
                                    }
                                  });
            });
        }

        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Frakton Proficiency Test API");
            });
        }
    }
}
