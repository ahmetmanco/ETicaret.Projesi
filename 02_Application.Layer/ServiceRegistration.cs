using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace _02_Application.Layer
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
