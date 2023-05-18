using Microsoft.Extensions.DependencyInjection;
using ShortLink.Application.Interfaces;
using ShortLink.Application.Services;
using ShortLink.Domain.Interface;
using ShortLink.Infra.Data.Repositories;

namespace ShortLink.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region repository
            services.AddScoped<ILinkRepository, LinkRepository>();
            #endregion

            #region service
            services.AddScoped<ILinkService, LinkService>();
            #endregion

  
        }
    }
}
