using Core.Services;
using Core.Settings;
using Models;

namespace Core.Specifications
{
    public class CountedProducts : SpecificationService<Product>
    {
        public CountedProducts(ProductSearchParameters parameters)
            : base(product =>
                (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search)) &&
                (!parameters.BrandId.HasValue || product.ProductBrandId == parameters.BrandId) &&
                (!parameters.TypeId.HasValue || product.ProductTypeId == parameters.TypeId)
            )
        { }
    }
}