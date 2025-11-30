using GData.DTOs.UserDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Services.Users;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<User>> Register(RegisterUserDTO request)
        {

            try
            {
                var result = await authServices.RegisterService(request);
                return Ok(result);
            }
            catch(FormatException formatException)
            {

                logger.LogError(formatException, $"Bad Request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad Request",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }

        }

        [HttpGet("get-User-By-Username")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {

            try
            {

                var result = await authServices.GetUserByUsernameService(username);
                return Ok(result);

            }
            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Bad Request");
                return Problem(

                    detail: nullException.Message,
                    title: "Bad Request",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Not found");
                return Problem(

                    detail: formatException.Message,
                    title: "Not Found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }

        }

        [HttpGet("get-User-By-Id{Id}")]
        public async Task<ActionResult<User>> GetUserById(Guid Id)
        {

            try
            {

                var result = await authServices.GetUserByIdService(Id);
                return Ok(result);

            }
            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Bad Request");
                return Problem(

                    detail: nullException.Message,
                    title: "Bad Request",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Not found");
                return Problem(

                    detail: formatException.Message,
                    title: "Not Found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
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

                logger.LogError(ex, $"User {request.Username} was not found!");
                return Problem(

                    detail: ex.Message,
                    title: "Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch(FormatException formatException)
            {

                logger.LogError(formatException, $"Bad Request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad Request",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized,Type=typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]

        public async Task<ActionResult<User>> ChangePassword(ChangePasswordDTO request)
        {

            try
            {

                var result = await authServices.ChangePasswordService(request);
                return Ok(result);

            }

            catch( UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not found");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }


        }
        [HttpGet("get-All-Users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {

            var result = await authServices.GetAllUsersService();

            if (result.Count < 1)
            {

                return NoContent();

            }
            else
            {
            
                return Ok(result);
            
            }
        }

        [HttpPatch("resend-User-Verification-code{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<User>> ResendVerificationCode(Guid Id)
        {
            try
            {
                var result = await authServices.ResendVerificationCodeService(Id);
                return Ok(result);
            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");

                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

    }
}
