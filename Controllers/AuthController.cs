using GData.DTOs;
using GData.Entity;
using GData.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {

        [HttpPost("register-User")]
        public async Task<ActionResult<User>> Register(RegisterUserDTO request)
        {

            var result = await authServices.RegisterService(request);

            if (result is not null)
            {

                return Ok(result);

            }
            else
            {

                return BadRequest();

            }

        }

        [HttpGet("get-User-By-Username")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {

            var result = await authServices.GetUserByUsernameService(username);

            if(result!=null)
            {

                return Ok(result);

            }
            else
            {

                return NotFound();

            }    
        }

        [HttpGet("get-User-By-Id{Id}")]
        public async Task<ActionResult<User>> GetUserById(Guid Id)
        {

            var result = await authServices.GetUserByIdService(Id);

            if (result != null)
            {

                return Ok(result);

            }
            else
            {

                return NotFound();

            }
        }

        [HttpPost("verify-Account")]
        public async Task<ActionResult<bool>> VerifyAccount(Guid Id, int code)
        {

            var result = await authServices.VerifyAccountService(Id, code);

            if(result is true)
            {

                return Ok(result);

            }
            else
            {

                return BadRequest("Wrong code or user!");

            }
        }

        [HttpPost("login-User")]

        public async Task<ActionResult<TokenDTO>> Login(LoginUserDTO request)
        {

            var result= await authServices.LoginService(request);

            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [Authorize]
        [HttpGet("authenticated-probe")]
        public async Task<ActionResult<User>> AuthenticatedEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize]
        [HttpPatch("change-Password")]
        public async Task<ActionResult<User>> ChangePassword(ChangePasswordDTO request)
        {

            var result=await authServices.ChangePasswordService(request);

            if(result is not null)
            {

                return Ok(result);  

            }
            else
            {

                return BadRequest();

            }

        }
    }
}
