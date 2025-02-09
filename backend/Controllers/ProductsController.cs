using Microsoft.AspNetCore.Mvc;
using GameStore.Models;
using GameStore.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductRepository _repository;

        public ProductsController(ILogger<ProductsController> logger, ProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repository.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _repository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] List<Product> products)
        {
            await _repository.BulkCreate(products);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllProducts()
        {
            var result = await _repository.DeleteAll();

            return Ok(result);
        }
    }
}
