using API.Context;
using API.IRepository;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;
        public UserController(IUserRepository userRepository , TokenService tokenService , AppDbContext context) 
        { 
            this._userRepository = userRepository;
            this._tokenService = tokenService;
            this._context = context;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<string>> login([FromBody] User user)
        {
            User userData = await this._userRepository.login(user.Email , user.Password);
            if (userData is null)
            {
                return Unauthorized();
            }

            string token = this._tokenService.GenerateToken(userData);
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public IQueryable<User> get()
        {
            return this._context.User.AsQueryable();
        }

        [HttpPost("/register")]
        public async Task<ActionResult<User>> register([FromBody] User user)
        {
            if (this._userRepository.userExists(user))
            {
                return BadRequest();
            }

            await this._userRepository.register(user);
            return CreatedAtAction("register", new { id = user.UserId }, user);
        }
    }
}
