using GData.DTOs;
using GData.Entity;

namespace GData.Exceptions
{
    public class UserExceptionList
    {

        public Task<TokenDTO> InvalidLoginData()
        {

            throw new FormatException("Invalid login data");

        }

        public Task<TokenDTO> UserDoesNotExist()
        {

            throw new ArgumentNullException("The requested user does not exist!");

        }
        public Task<TokenDTO> InternalServerError()
        {

            throw new Exception("An unexpected error occured! Please try again later!");

        }

    }
}
