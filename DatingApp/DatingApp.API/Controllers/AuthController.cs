using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOS;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userDto)
        {
            userDto.Username = userDto.Username.ToLower();

            if(await _repo.IsUserExists(userDto.Username))
            {
                return BadRequest("Username already taken");
            }

            var newUser = new User 
            {
                UserName = userDto.Username

            };

            var createdUser = await _repo.Register(newUser, userDto.Password);
            return StatusCode(201);           
        }

    }
}