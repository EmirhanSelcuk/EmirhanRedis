using EmirhanRedis.API.Model;
using EmirhanRedis.API.Repositories;
using EmirhanRedisApp.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmirhanRedis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;  
        
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
        
                var products = await _productRepository.GetAsync();

                if (products != null && products.Any())
                {
                    var response = ApiResponse<List<Product>>.SuccessResponse(products, "Ürünler başarıyla getirildi.");
                    return Ok(response);  // 200 OK ile birlikte başarılı yanıt
                }

                var errorResponse = ApiResponse<List<Product>>.ErrorResponse("Ürünler getirilemedi.");
                return NotFound(errorResponse);  // 404 Not Found
            }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
               
                var product = await _productRepository.GetByIdAsync(id);

                if (product != null)
                {
                    var successResponse = ApiResponse<Product>.SuccessResponse(product, "Ürün başarıyla bulundu.");
                    return Ok(successResponse);  // 200 OK ile birlikte başarılı yanıt
                }

                var errorResponse = ApiResponse<Product>.ErrorResponse("Ürün bulunamadı.");
                return NotFound(errorResponse);  // 404 Not Found
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                var createdProduct = await _productRepository.CreateAsync(product);

                // Başarılı ürün oluşturma yanıtı
                var successResponse = ApiResponse<Product>.SuccessResponse(createdProduct, "Ürün başarıyla oluşturuldu.");
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, successResponse);  // 201 Created
            }
            catch (Exception ex)
            {
                // Hata durumunda yanıt
                var errorResponse = ApiResponse<Product>.ErrorResponse($"Ürün oluşturulurken hata oluştu: {ex.Message}");
                return BadRequest(errorResponse);  // 400 Bad Request
            }
        }

    }
}
