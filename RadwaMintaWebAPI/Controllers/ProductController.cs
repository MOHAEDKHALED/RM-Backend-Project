using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Products;

namespace RadwaMintaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get All Products
        [HttpGet("GetFirst4Products")]
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetFirst4Products(string lang)
        {
            var products = await _serviceManager.ProductService.GetFirst4ProductsAsync(lang);
            return Ok(products);
        }

        [HttpGet("AllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetAllProducts(string lang)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(lang);
            return Ok(products);
        }

        [HttpGet("AllCategories")] 
        public async Task<ActionResult<IEnumerable<CategoryDTo>>> GetAllCategories(string lang)
        {
            var categories = await _serviceManager.ProductService.GetAllCategoriesAsync(lang);
            return Ok(categories);
        }

        [HttpGet("ProductsByCategoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetProductsByCategoryId(int categoryId, string lang)
        {
            var products = await _serviceManager.ProductService.GetProductsByCategoryIdAsync(categoryId, lang);
            return Ok(products);
        }

        [HttpGet("RelatedProductsByCategoryId/{categoryId}/Exclude/{currentProductId}")]
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetRelatedProductsByCategoryId(int categoryId, int currentProductId, string lang)
        {
            var products = await _serviceManager.ProductService.GetRelatedProductsByCategoryIdAsync(categoryId, currentProductId, lang);
            return Ok(products);
        }


        // Get Products By Id
        [HttpGet("ProductsById{id}")]
        public async Task<ActionResult<ProductDTo>> GetProduct(int id, string lang)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id, lang);
            return Ok(Product);
        }


        // For Order


        [HttpGet("whatsapp-order-link/{productId}")]
        public async Task<IActionResult> GenerateWhatsappOrderLink(int productId)
        {
            var whatsappLink = await _serviceManager.ProductService.GenerateWhatsappLinkForOrder(productId);

            return Ok(new { whatsappLink = whatsappLink });
        }

    }
}
