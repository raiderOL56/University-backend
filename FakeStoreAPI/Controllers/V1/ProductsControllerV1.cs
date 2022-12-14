using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeStoreAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private const string _URL = "https://fakestoreapi.com/products";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("1.0")]
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