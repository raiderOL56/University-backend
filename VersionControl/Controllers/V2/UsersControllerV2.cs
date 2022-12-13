using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using VersionControl.DTO;

namespace VersionControl.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private const string _ApiURL = "https://dummyapi.io/data/v1/user?limit=";
        private const string _AppID = "6396791d152cc013982dc1aa";
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetUsersData/{limit}")]
        public async Task<IActionResult> GetUsersDataAsync(int limit)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("app-id", _AppID);

            Stream response = await _httpClient.GetStreamAsync(string.Concat(_ApiURL, limit));

            UserResponseData? usersResponseData = await JsonSerializer.DeserializeAsync<UserResponseData>(response);

            List<UserData>? usersData = usersResponseData?.data;

            return Ok(usersData);
        }
    }
}