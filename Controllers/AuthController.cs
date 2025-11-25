using GData.DTOs;
using GData.Entity;
using GData.Services;
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

            var result=await authServices.RegisterService(request);
            
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
