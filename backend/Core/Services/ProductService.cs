using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _databaseContext;

        public ProductService(DatabaseContext databaseContext) => _databaseContext = databaseContext;

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync() =>
            await _databaseContext.ProductBrands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id) =>
            await _databaseContext.Products
                .Include(product => product.ProductType)
                .Include(product => product.ProductBrand)
                .FirstOrDefaultAsync(product => product.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync() =>
            await _databaseContext.Products
                .Include(product => product.ProductType)
                .Include(product => product.ProductBrand)
                .ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() =>
            await _databaseContext.ProductTypes.ToListAsync();
    }
}