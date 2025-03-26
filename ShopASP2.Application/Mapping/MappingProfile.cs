using AutoMapper;
using ShopASP2.Application.DTO;
using ShopASP2.Domain.Entities;

namespace ShopASP2.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
        }
    }
}
