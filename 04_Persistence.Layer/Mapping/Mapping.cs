using _01_Domain.Layer.Entities;
using _02_Application.Layer.Features.Commands.AppUser.CreateUser;
using _02_Application.Layer.Features.Commands.Product.CreateProduct;
using AutoMapper;

namespace _04_Persistence.Layer.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, CreateProductCommandRequest>().ReverseMap();
            CreateMap<AppUser, CreateUserCommandRequest>().ReverseMap();
        }
    }
}
