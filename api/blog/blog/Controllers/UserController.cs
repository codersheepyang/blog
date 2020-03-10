using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using blog.inputs;
using blog.Models.Consumer;
using blog.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace blog.Controllers
{
  
    [Produces("application/json")]
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _loginService;

        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(UserController));

        public IConfiguration Configuration { get; }

        public UserController(IConfiguration configuration, IUserService loginService)
        {
            Configuration = configuration;
            _loginService = loginService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public void Login() {
            log.Info("get方法");
        }
        [HttpPost("register")]
        public ActionResult<string> Register(User user)
        {
            string username = user.Username;
            string password = user.Password;
            if (username == null || password == null)
            {
                return Ok(null);
            }
            log.Info("username=" + username + ",password=" + password);
            string result = _loginService.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public ActionResult<string> Login (User login)
        {
            string username = login.Username;
            string password = login.Password;
            if (username == null || password == null)
            {
                return NotFound(null);
            }
            log.Info("username=" + username + ",password=" + password);
            //密码校验正确，可获得Token
            User user = _loginService.CheckLogin(username, password);
            if (user != null)
            {
                var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim(ClaimTypes.Role, "Admin"),
            };
                //7天的Token有效期
                var token = new JwtSecurityToken
                (
                    issuer: Configuration["Tokens:ValidIssuer"],
                    audience: Configuration["Tokens:ValidAudience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Tokens:IssuerSigningKey"])),
                    SecurityAlgorithms.HmacSha256)
                );
                return Ok(user.ID);
            }
            return Ok(null);
        }
    }
}