using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VersionControl.DTO;

namespace VersionControl.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private const string _ApiURL = $"https://dummyapi.io/data/v1/user?limit=";
        private const string _AppID = "6397dde092bf13ebae9aa37e";
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("1.0")]
        [HttpGet(Name = "GetUsersData/{limit}")]
        public async Task<IActionResult> GetUsersDataAsync(int limit)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("app-id", _AppID);

                UserResponseData? userResponseData = await _httpClient.GetFromJsonAsync<UserResponseData>(string.Concat(_ApiURL, limit));

                return Ok(userResponseData);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}