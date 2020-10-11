using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using Core.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Settings;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductViewModel>>> GetProducts([FromQuery] ProductSearchParameters productSearchParameters)
        {
            var includeBrandsAndTypesQuery = new ProductsIncludingTypesAndBrands(productSearchParameters);
            var countParameters = new CountedProducts(productSearchParameters);
            var totalItems = await _unitOfWork.Service<Product>().CountAsync(countParameters);
            var products = await _unitOfWork.Service<Product>().ListAllWithSpecificationAsync(includeBrandsAndTypesQuery);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(products);

            return Ok(new Pagination<ProductViewModel>(
                productSearchParameters.PageIndex,
                productSearchParameters.PageSize,
                totalItems,
                data
            ));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            var includeBrandsAndTypesQuery = new ProductsIncludingTypesAndBrands(id);
            var product = await _unitOfWork.Service<Product>().GetOneWithSpecification(includeBrandsAndTypesQuery);

            if (product == null) return NotFound(new APIResponse(StatusCodes.Status404NotFound));

            return _mapper.Map<Product, ProductViewModel>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() =>
            Ok(await _unitOfWork.Service<ProductBrand>().ListAllAsync());

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() =>
            Ok(await _unitOfWork.Service<ProductType>().ListAllAsync());
    }
}