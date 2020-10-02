using Core.Services;
using Models;

namespace Core.Specifications
{
    public class ProductsIncludingTypesAndBrands : SpecificationService<Product>
    {
        public ProductsIncludingTypesAndBrands()
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }

        public ProductsIncludingTypesAndBrands(int id) : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
}