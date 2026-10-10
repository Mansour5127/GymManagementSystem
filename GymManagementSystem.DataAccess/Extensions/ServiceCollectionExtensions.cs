using GymManagementSystem.DataAccess.Data.Contexts;
using GymManagementSystem.DataAccess.InterceptorsSENTINEL;
using GymManagementSystem.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGymDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<AuditSaveChangesInterceptor>();

            services.AddDbContext<GymDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetRequiredService<AuditSaveChangesInterceptor>());
            });

            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            //builder.Services.AddKeyedScoped<PlanRepository>("PlanRepo");


            return services;
        }
    }
}
