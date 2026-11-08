using GymManagementSystem.BusinessLogic.Servives;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            return services;
        }
    }
}
