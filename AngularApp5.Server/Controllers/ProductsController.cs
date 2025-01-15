using AngularApp5.Server.Data;
using AngularApp5.Server.Dtos;
using AngularApp5.Server.Errors;
using AngularApp5.Server.Helpers;
using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Models;
using AngularApp5.Server.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularApp5.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductBrand> productBrandsRepo;
        private readonly IGenericRepository<ProductType> productTypesRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandsRepo,
            IGenericRepository<ProductType> productTypesRepo)
        {
            this.productsRepo = productsRepo;
            this.productBrandsRepo = productBrandsRepo;
            this.productTypesRepo = productTypesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await productsRepo.CountAsync(countSpec);
            var products = await productsRepo.ListWithSpecAsync(spec);

            var data = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductType = product.ProductType.Name,
                ProductBrand = product.ProductBrand.Name
            }).ToList();

            return Ok(new Pagination<ProductDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await productsRepo.GetModelWithSpecAsync(spec);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductType = product.ProductType.Name,
                ProductBrand = product.ProductBrand.Name
            };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await productBrandsRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await productTypesRepo.ListAllAsync());
        }
    }
}
