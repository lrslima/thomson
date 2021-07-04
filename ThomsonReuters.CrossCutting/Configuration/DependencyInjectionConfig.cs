using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Business.Interfaces;
using ThomsonReuters.Business.Services;
using ThomsonReuters.Infra.Repository;

namespace ThomsonReuters.CrossCutting.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ILegalCaseRepository, LegalCaseRepository>();
            services.AddScoped<ILegalCaseService, LegalCaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtUtils, JwtUtils>();

        }
    }
}
