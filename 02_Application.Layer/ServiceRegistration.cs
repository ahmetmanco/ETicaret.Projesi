
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace _02_Application.Layer
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
            });
        }
    }
}
