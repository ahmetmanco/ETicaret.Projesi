using Microsoft.Extensions.DependencyInjection;
using MediatR;
namespace _02_Application.Layer
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
