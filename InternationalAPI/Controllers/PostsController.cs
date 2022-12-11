using InternationalAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InternationalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IStringLocalizer<PostsController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public PostsController(IStringLocalizer<PostsController> stringLocalizer, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        [HttpGet]
        [Route("PostsControllerResource")]
        public IActionResult GetUsingPorstControllerResource()
        {
            // Find text
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? string.Empty;
            return Ok(new
            {
                PostType = article.Value,
                PostName = postName
            });
        }

        [HttpGet]
        [Route("SharedResource")]
        public IActionResult GetUsingSharedResource()
        {
            // Find text
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? string.Empty;
            var todayIs = string.Format(_sharedResourceLocalizer.GetString("TodayIs"), DateTime.Now.ToLongDateString());

            return Ok(new
            {
                PostType = article.Value,
                PostName = postName,
                TodayIs = todayIs
            });
        }
    }
}