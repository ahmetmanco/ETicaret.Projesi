using _01_Domain.Layer.Enums;
using _02_Application.Layer.Abstraction.Storage;
using _02_Application.Layer.Abstraction.Storage.Azure;
using _03_Infrastructure.Layer.Services.Storage;
using _03_Infrastructure.Layer.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace _03_Infrastructure.Layer
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureservices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;

                case StorageType.AWS:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

                default:
                    throw new NotImplementedException($"{storageType} storage type is not implemented.");
            }
        }
    }
}
