using Microsoft.Extensions.DependencyInjection;
using Service.Implement;
using Service.Interface;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IAuthService, AuthService>();
            service.AddScoped<IEmailSender, EmailSenderService>();

            return service;
        }
    }
}
