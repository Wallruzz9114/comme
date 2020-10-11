using Core.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Models;

namespace API.Helpers
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductViewModel, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureURLResolver(IConfiguration configuration) => _configuration = configuration;

        public string Resolve(Product source, ProductViewModel destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
                return _configuration["ApiURL"] + source.PictureURL;

            return null;
        }
    }
}