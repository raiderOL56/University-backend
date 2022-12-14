using FakeStoreAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreAPI.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private const string _URL = "https://fakestoreapi.com/products?limit=5";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                List<Product>? products = await _httpClient.GetFromJsonAsync<List<Product>>(_URL);

                return Ok(products);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}