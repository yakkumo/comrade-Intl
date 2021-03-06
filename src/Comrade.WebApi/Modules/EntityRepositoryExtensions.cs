#region

using Comrade.Core.AirplaneCore;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Comrade.WebApi.Modules
{
    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class EntityRepositoryExtensions
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddEntityRepository(
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAirplaneRepository, AirplaneRepository>();
            services.AddScoped<ISystemUserRepository, SystemUserRepository>();

            return services;
        }
    }
}