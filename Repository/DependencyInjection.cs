using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DataAccess.Implement;
using Repository.DataAccess.Interface;
using Repository.SupabaseFileUploader;

namespace Repository
{
    /*
     * Dependency injection (DI) is a technique for achieving loose coupling between objects and their collaborators, or dependencies. 
     * Most often, classes will declare their dependencies via their constructor, allowing them to follow the Explicit Dependencies Principle. 
     * This approach is known as "constructor injection".
     * To implement dependency injection, we need to configure a DI container with classes that is participating in DI. 
     * DI Container has to decide whether to return a new instance of the service or provide an existing instance.
     */
    public static class DependencyInjection
    {
    /*
     * The below three methods define the lifetime of the services:
     *  - AddTransient: Transient lifetime services are created each time they are requested. 
     * This lifetime works best for lightweight, stateless services.
     *  - AddScoped: Scoped lifetime services are created once per request.
     *  - AddSingleton: Singleton lifetime services are created the first time they are requested 
     * (or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance.
     */
    public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IAuthRepository, AuthRepository>();
            service.AddScoped<IAuthRepository, AuthRepository>();
            service.AddTransient<UploadFile>();

            return service;
        }
    }
}
