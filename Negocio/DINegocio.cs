using Datos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Negocio.Interfaces;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class DINegocio
    {
        public static void AddDINegocio(this IServiceCollection services)
        {
            services.AddScoped<ITareaService, TareaService>();
        }
    }
}
