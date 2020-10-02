using System.Collections.Generic;
using System.Threading.Tasks;
using API.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<ProductBrand> _producBrandService;
        private readonly IGenericService<ProductType> _productTypeService;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericService<Product> productService,
            IGenericService<ProductBrand> producBrandService,
            IGenericService<ProductType> productTypeService,
            IMapper mapper)
        {
            _productService = productService;
            _producBrandService = producBrandService;
            _productTypeService = productTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductViewModel>>> GetProducts()
        {
            var includeBrandsAndTypesQuery = new ProductsIncludingTypesAndBrands();
            var products = await _productService.ListAllWithSpecificationAsync(includeBrandsAndTypesQuery);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            var includeBrandsAndTypesQuery = new ProductsIncludingTypesAndBrands(id);
            var product = await _productService.GetOneWithSpecification(includeBrandsAndTypesQuery);

            return _mapper.Map<Product, ProductViewModel>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
            Ok(await _producBrandService.ListAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() =>
            Ok(await _productTypeService.ListAllAsync());
    }
}