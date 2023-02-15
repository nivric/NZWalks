using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository,ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest loginRequest)
        {
            //Validate input

            //call repository method and authenticate
            var user =await userRepository.AuthenticateUserAsync(loginRequest.UserName, loginRequest.Password);

            if (user == null) 
            {
                return BadRequest("Invalid username or password");
            }

            //Generate JWT token
            return Ok(tokenHandler.GetTokenAsync(user).Result);
        }
    }
}
