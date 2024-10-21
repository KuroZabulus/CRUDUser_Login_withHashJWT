using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DataAccess.Implement;
using Repository.DataAccess.Interface;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IAuthRepository, AuthRepository>();

            return service;
        }
    }
}
