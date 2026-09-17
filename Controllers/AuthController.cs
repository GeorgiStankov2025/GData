// AuthController.cs
using GData.DTOs.UserDTO;
using GData.Entity;
using GData.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO request)
        {
            var result = await authServices.RegisterService(request);
            return Ok(result);
        }

        [HttpGet("by-username")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
        {
            var result = await authServices.GetUserByUsernameService(username);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await authServices.GetUserByIdService(id);
            return Ok(result);
        }

        [HttpPost("{id:guid}/verify")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyAccount(Guid id, [FromQuery] int code)
        {
            var result = await authServices.VerifyAccountService(id, code);
            return result ? Ok(true) : BadRequest("Wrong code or user!");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
        {
            var result = await authServices.LoginService(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("authenticated-probe")]
        public IActionResult AuthenticatedEndpoint() => Ok("You are authenticated!");

        [Authorize]
        [HttpPatch("{id:guid}/change-password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDTO request)
        {
            var result = await authServices.ChangePasswordService(id, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await authServices.GetAllUsersService();
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpPost("{id:guid}/resend-verification-code")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> ResendVerificationCode(Guid id)
        {
            var result = await authServices.ResendVerificationCodeService(id);
            return Ok(result);
        }
    }
}