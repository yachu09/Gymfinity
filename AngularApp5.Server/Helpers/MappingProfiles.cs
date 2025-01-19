using AngularApp5.Server.Models.OrderAggregate;
using AngularApp5.Server.Models;
using AngularApp5.Server.Models.Identity;
using AngularApp5.Server.Dtos;
using AutoMapper;

namespace AngularApp5.Server.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));
                //.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Models.Identity.Address, AddressDto>().ReverseMap();
            //CreateMap<CustomerBasketDto, CustomerBasket>();
            //CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Models.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ItemOrdered.ImageUrl));
        }
    }
}