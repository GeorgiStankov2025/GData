using GData.DTOs.UserDTO;
using GData.Entity;

namespace GData.Exceptions
{
    public class UserExceptionList
    {
        
        //Login Errors
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

        //Register Errors

        public Task<User> ErrorProcessingRequest()
        {

            throw new ArgumentNullException("No data submitted in request");

        }

        public Task<User> UsernameAlreadyExists()
        {

            throw new FormatException("There is already an account with this username");

        }

        public Task<User> EmailAlreadyExists()
        {

            throw new FormatException("There is already an account with this email");

        }

        public Task<User> FillAllBoxes()
        {

            throw new FormatException("All fields are required");

        }

        public Task<User> InvalidData()
        {

            throw new FormatException("The data you submitted is inconsistent! Username and password need to contain at least 8 characters. First name and Last name need to contain at least two charcters");

        }

        public Task<User> InvalidEmail()
        {

            throw new FormatException("The email address you provided is invalid or does not exist");

        }

        //GetUserByUsername Errors

        public Task<User> NoRequestUsername()
        {

            throw new ArgumentNullException("No username has been provided");

        }

        public Task<User> UserNotFoundWithUsername()
        {

            throw new FormatException("No user was found with this username");

        }

        //GetUserById Errors

        public Task<User> NoRequestId()
        {

            throw new ArgumentNullException("No Id has been provided");

        }

        public Task<User> UserNotFoundWithId()
        {

            throw new FormatException("No user was found with this Id");

        }

        //ChangePassword Errors

        public Task<User> ErrorProcessingRequestChangePass()
        {

            throw new ArgumentNullException("No data submitted in request");

        }

        public Task<User> FillAllBoxesChangePass()
        {

            throw new FormatException("All fields are required");

        }

        public Task<User> InvalidNewPassword()
        {

            throw new FormatException("The new password needs to have at least 8 characters");
        }

        public Task<User> SamePassword()
        {

            throw new FormatException("The new password cannot be the same as the old one");

        }

        public Task<User> EmailNotVerified()
        {

            throw new FormatException("The submitted email is not verified");

        }

        public Task<User> InvalidUserCredentials()
        {

            throw new FormatException("Incorrect user credentials");

        }

        public Task<User> InvalidUser()
        {

            throw new UnauthorizedAccessException("The requested user is not the account owner");

        }

        //Resend Verification code errors

        public Task<User> AccountIsVerified()
        {

            throw new FormatException("Account is already verified");

        }

        //Favourites errors

        public Task<User> ArticleNotFound()
        {

            throw new ArgumentNullException("The requested article does not exist or was deleted");

        }

        public Task<User> InvalidPersonalDataEdit()
        {

            throw new UnauthorizedAccessException("Only the person himself can change his personal data.");

        }

        public Task<User> UserNotFound()
        {

            throw new ArgumentNullException("The requested user does not exist!");

        }

    }
}
