using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTO_s.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
