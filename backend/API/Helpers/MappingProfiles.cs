using API.ViewModels;
using AutoMapper;
using Models;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(pwm => pwm.ProductBrand, mce => mce.MapFrom(p => p.ProductBrand.Name))
                .ForMember(pwm => pwm.ProductType, mce => mce.MapFrom(p => p.ProductType.Name))
                .ForMember(pwm => pwm.PictureURL, mce => mce.MapFrom<ProductPictureURLResolver>());
        }
    }
}