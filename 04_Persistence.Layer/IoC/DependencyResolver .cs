using _02_Application.Layer.Repositories.Costumer;
using _02_Application.Layer.Repositories;
using Autofac;
using AutoMapper;
using _02_Application.Layer.Repositories.AppUser;
using _04_Persistence.Layer.Repositories;
using _04_Persistence.Layer.Repositories.Address;
using _01_Domain.Layer.Entities;
using _04_Persistence.Layer.Context;

namespace _04_Persistence.Layer.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterType<AddreesReadRepository>().As<IAddreesReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AddressWriteRepository>().As<IAddressWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CityReadRepository>().As<ICityReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityWriteRepository>().As<ICityWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<DistrictReadRepository>().As<IDistrictReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DistrictWriteRepository>().As<IDistrictWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserReadRepository>().As<IAppUserReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserWriteRepository>().As<IAppUserWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CustomerReadRepository>().As<ICustomerReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerWriteRepository>().As<ICustomerWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ProductReadRepository>().As<IProductReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductWriteRepository>().As<IProductWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<OrderReadRepository>().As<IOrderReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderWriteRepository>().As<IOrderWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<FileReadRepository>().As<IFileReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FileWriteRepository>().As<IFileWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ProductImageReadRepository>().As<IProductImageFileReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductImageWriteRepository>().As<IProductImageFileWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<InvoiceFileReadRepository>().As<IInvoiceFileReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceFileWriteRepository>().As<IInvoiceFileWriteRepository>().InstancePerLifetimeScope();

            builder.Register(c =>
            {
                
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
