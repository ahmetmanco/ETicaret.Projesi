using _01_Domain.Layer.Entities;
using _02_Application.Layer.VMs;
using AutoMapper;

namespace _04_Persistence.Layer.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, CreateProductVM>().ReverseMap();
            CreateMap<Product, UpdateProductVM>().ReverseMap();
        }
    }
}
