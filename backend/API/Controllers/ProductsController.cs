using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) => _productService = productService;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            Ok(await _productService.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) =>
            await _productService.GetProductByIdAsync(id);

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
            Ok(await _productService.GetProductBrandsAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() =>
            Ok(await _productService.GetProductTypesAsync());
    }
}