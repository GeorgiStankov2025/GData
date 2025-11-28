using GData.DTOs;
using GData.Entity;
using GData.Exceptions;
using GData.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices, ILogger<AuthController> logger) : ControllerBase
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TokenDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TokenDTO>> Login(LoginUserDTO request)
        {
            try
            {

                var result = await authServices.LoginService(request);
                return Ok(result);

            }
            catch(ArgumentNullException ex)
            {

                logger.LogError(ex, $"Not found!");
                return Problem(

                    detail: $"User not found",
                    title: "Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch(FormatException formatException)
            {

                logger.LogError(formatException, $"Bad Request");
                return Problem(

                    detail: "Invalid login data!",
                    title: "Bad Request",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: "An unexpected error occured while proccessing your request",
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

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
        [HttpGet("get-All-Users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {

            return Ok(await authServices.GetAllUsersService());

        }
    }
}
