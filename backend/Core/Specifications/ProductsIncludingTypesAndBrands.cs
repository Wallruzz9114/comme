using Core.Services;
using Core.Settings;
using Models;

namespace Core.Specifications
{
    public class ProductsIncludingTypesAndBrands : SpecificationService<Product>
    {
        public ProductsIncludingTypesAndBrands(ProductSearchParameters parameters)
            : base(product =>
                (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search)) &&
                (!parameters.BrandId.HasValue || product.ProductBrandId == parameters.BrandId) &&
                (!parameters.TypeId.HasValue || product.ProductTypeId == parameters.TypeId)
            )
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
            AddOrderBy(product => product.Name);

            if (!string.IsNullOrEmpty(parameters.Sort))
                switch (parameters.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }

            Paginate(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);
        }

        public ProductsIncludingTypesAndBrands(int productId) : base(product => product.Id == productId)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
}