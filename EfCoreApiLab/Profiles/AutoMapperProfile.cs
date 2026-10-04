using AutoMapper;
using EfCoreApiLab.Models;

namespace EfCoreApiLab.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}