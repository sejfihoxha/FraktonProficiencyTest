using FraktonProficiencyTest.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProficiencyTest.Extensions
{
    public static class RegisterDependencyInjectionExtension
    {
        public static void RegisterServiceDependencies(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);

            var types = serviceInterfaceType
                     .Assembly
                     .GetExportedTypes()
                     .Where(t => t.IsClass && !t.IsAbstract)
                     .Select(t => new
                     {
                         Service = t.GetInterface($"I{t.Name}"),
                         Implementation = t
                     })
                     .Where(t => t.Service != null);

            foreach (var type in types)
            {
                services.AddScoped(type.Service, type.Implementation);
            }
        }
    }
}
