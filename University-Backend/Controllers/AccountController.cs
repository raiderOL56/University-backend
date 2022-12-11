using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University_Backend.Helpers;
using University_Backend.Models.DataModels;
using University_Backend.Models.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using University_Backend.Data;

namespace University_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly JWTsettings _jwtSettings;
        private readonly DbContext_University _context;
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "martin@example.com",
                Name = "Admin",
                Password = "admin"
            },
            new User()
            {
                Id = 2,
                Email = "pepe@example.com",
                Name = "User",
                Password = "user"
            }
        };

        public AccountController(JWTsettings jwtSettings, DbContext_University context)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        [HttpPost]
        public IActionResult GetToken(UserLoginDTO userLoginDTO)
        {
            try
            {
                // Search a user in context with LINQ
                User? user = (from userItem in _context.Users
                                 where userItem.Name == userLoginDTO.Username && userItem.Password == userLoginDTO.Password
                                 select userItem).FirstOrDefault();

                if (user != null)
                {

                    UserToken userToken = JwtHelper.GenerateToken(new UserToken()
                    {
                        Username = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);

                    return Ok(userToken);
                }
                else
                {
                    return BadRequest("Wrong credentials");
                }
            }
            catch (System.Exception e)
            {
                throw new Exception($"Get token error: {e.Message}");
            }
        }
    }
}