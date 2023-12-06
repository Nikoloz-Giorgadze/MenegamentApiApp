using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementApi.Services.UserProfileService;
using ManagementApi.Services.UserService;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ManagementApi.Services.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService.UserService>();
            services.AddScoped<IUserProfileService, UserProfileService.UserProfileService>();

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
