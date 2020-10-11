using Core.ViewModels;
using AutoMapper;
using Models;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(pvm => pvm.ProductBrand, mce => mce.MapFrom(p => p.ProductBrand.Name))
                .ForMember(pvm => pvm.ProductType, mce => mce.MapFrom(p => p.ProductType.Name))
                .ForMember(pvm => pvm.PictureURL, mce => mce.MapFrom<ProductPictureURLResolver>());

            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<AddressViewModel, ShippingAddress>();
            CreateMap<CartViewModel, Cart>();
            CreateMap<CartItemViewModel, CartItem>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(ovm => ovm.DeliveryMethod, mce => mce.MapFrom(o => o.DeliveryMethod.Name))
                .ForMember(ovm => ovm.DeliveryMethodPrice, mce => mce.MapFrom(o => o.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(oivm => oivm.ProductId, mce => mce.MapFrom(oi => oi.ProductItemOrdered.ProductItemId))
                .ForMember(oivm => oivm.ProductName, mce => mce.MapFrom(oi => oi.ProductItemOrdered.ProductName))
                .ForMember(oivm => oivm.PictureURL, mce => mce.MapFrom(oi => oi.ProductItemOrdered.PictureURL))
                .ForMember(oivm => oivm.PictureURL, mce => mce.MapFrom<OrderItemURLResolver>());
        }
    }
}