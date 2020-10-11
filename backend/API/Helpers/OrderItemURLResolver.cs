using AutoMapper;
using Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Models;

namespace API.Helpers
{
    public class OrderItemURLResolver : IValueResolver<OrderItem, OrderItemViewModel, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemURLResolver(IConfiguration configuration) => _configuration = configuration;

        public string Resolve(OrderItem source, OrderItemViewModel destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureURL))
                return _configuration["ApiURL"] + source.ProductItemOrdered.PictureURL;

            return null;
        }
    }
}