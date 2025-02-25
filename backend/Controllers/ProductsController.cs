using Microsoft.AspNetCore.Mvc;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using GameStore.Services;
using GameStore.Models.Request;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, ProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productsService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productsService.GetById(id);

            if (product == null)
            {
                return BadRequest();
            }

            return Ok(product);
        }


        [Authorize(Roles = "admin,manager")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateProduct([FromForm] ProductRequest product)
        {
            var result = await _productsService.Create(product);

            return Ok(result);
        }


        [Authorize(Roles = "admin,manager")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromForm] ProductRequest request)
        {
            var result = await _productsService.Update(request);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }


        [Authorize(Roles = "admin,manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var result = await _productsService.DeleteById(id);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
