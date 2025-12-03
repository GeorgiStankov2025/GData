using GData.DTOs.UserDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.Users;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;




namespace GData.Services.Users
{
    public class AuthServices(IAuthRepository authRepository, IConfiguration configuration, UserExceptionList exceptionList) : IAuthServices
    {
        private async void SendEmailRegistration(User user)
        {

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("bitproductions2024@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = $"GDataRegistration ";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {

                Text = $"Hi, {user.Username}! Wellcome to GData. You have successfully registered to our website. You can activate your account using the code: {user.VerificationCode} \n\n\n GData Team"

            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            await smtp.AuthenticateAsync("bitproductions2024@gmail.com", "unbv xvlo wrvs vgnm");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

        }

        public async Task<User> RegisterService(RegisterUserDTO request)
        {

            
                
            if(_ = new System.Net.Mail.MailAddress(request.Email) is not System.Net.Mail.MailAddress)
            {

                return await exceptionList.InvalidEmail();

            }

            List<User> users = await GetAllUsersService();

            if (request == null)
            {

               return await exceptionList.ErrorProcessingRequest();

            }

            foreach(var user in users)
            {

               if(request.Username==user.Username)
               {

                   return await exceptionList.UsernameAlreadyExists();

               }

               if(request.Email==user.Email)
               {

                  return await exceptionList.EmailAlreadyExists();

               }

            }

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Firstname) || string.IsNullOrWhiteSpace(request.Lastname))
            {

               return await exceptionList.FillAllBoxes();

            }
            if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Username) && string.IsNullOrWhiteSpace(request.Password) && string.IsNullOrWhiteSpace(request.Username) && string.IsNullOrWhiteSpace(request.Firstname) || string.IsNullOrWhiteSpace(request.Lastname))
            {

               return await exceptionList.FillAllBoxes();

            }
            if (request.Username.Length<8||request.Password.Length<8||request.Firstname.Length<2||request.Lastname.Length<2)
            {

               return await exceptionList.InvalidData();

            }
            if (request.Username.Length < 8 && request.Password.Length < 8 && request.Firstname.Length < 2 && request.Lastname.Length < 2)
            {

               return await exceptionList.InvalidData();
            
            }
             
            var result = await authRepository.Register(request);
            SendEmailRegistration(result);
            return result;
            
        }

        public async Task<bool> VerifyAccountService(Guid Id, int code)
        {

            var user = await authRepository.GetUserById(Id);

            if (user != null)
            {
                var result = await authRepository.VerifyAccount(user, code);

                if (result is true)
                {

                    return true;

                }

                else return false;
            }
            else return false;

        }

        public async Task<User> GetUserByUsernameService(string username)
        {

            if (string.IsNullOrWhiteSpace(username))
            {

                return await exceptionList.NoRequestUsername();

            }
            else
            {
                var result = await authRepository.GetUserByUsername(username);

                if (result is not null)
                {

                    return result;

                }
                else
                {

                    return await exceptionList.UserNotFoundWithUsername();

                }

            }

        }

        public async Task<User> GetUserByIdService(Guid Id)
        {
            if (Id == Guid.Empty)
            {

                return await exceptionList.NoRequestId();

            }
            else
            {
                var result = await authRepository.GetUserById(Id);
                if (result is not null)
                {

                    return result;

                }
                else
                {

                    return await exceptionList.UserNotFoundWithId();

                }

            }
        }

        private string CreateJWTToken(User user)
        {

            var claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(

                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds

            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }

        private async Task<TokenDTO> CreateJwtToken(User user)
        {

           var token= new TokenDTO { AccessToken = CreateJWTToken(user) };

           return token;

        }

        public async Task<TokenDTO> LoginService(LoginUserDTO request)
        {

            var user = await authRepository.GetUserByUsername(request.Username);

            var passwordHasher = new PasswordHasher<User>();

            if (user is null)
            {

                return await exceptionList.UserDoesNotExist();

            }
            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {

                return await exceptionList.InvalidLoginData();

            }
            
            return await CreateJwtToken(user);

        }

        public async Task<User> ChangePasswordService(ChangePasswordDTO request)
        {
            
            var user=await authRepository.GetUserByUsername(request.Username);

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            if(string.IsNullOrWhiteSpace(request.Username))
            {

                return await exceptionList.FillAllBoxesChangePass();

            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {

                return await exceptionList.FillAllBoxesChangePass();

            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {

                return await exceptionList.FillAllBoxesChangePass();

            }

            if (request.NewPassword.Length<8)
            {

                return await exceptionList.InvalidNewPassword();

            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {

                return await exceptionList.FillAllBoxesChangePass();

            }

            if(user is null)
            {

                return await exceptionList.ErrorProcessingRequestChangePass();

            }

            if(request.Username!=user.Username|| request.Email!=user.Email|| passwordHasher.VerifyHashedPassword(user,user.PasswordHash,request.Password )!= PasswordVerificationResult.Success)
            {

                return await exceptionList.InvalidUserCredentials();

            }

            if (request.Username != user.Username && request.Email != user.Email && passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
            {

                return await exceptionList.InvalidUserCredentials();

            }

            else if(passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.NewPassword) == PasswordVerificationResult.Success)
            {

                return await exceptionList.SamePassword();

            }

            if(user.IsEmailConfirmed==false)
            {

                return await exceptionList.EmailNotVerified();

            }

            await authRepository.ChangePassword(request.NewPassword, user);

            return user;

        }

        public async Task<List<User>> GetAllUsersService()
        {

            return await authRepository.GetAllUsers();

        }

        public async Task<User> ResendVerificationCodeService(Guid Id)
        {
 
            var user=await GetUserByIdService(Id);

            if (user.IsEmailConfirmed == false)
            {

                var result = await authRepository.ResendVerificationCode(user);

                SendEmailRegistration(user);

                return result;

            }
            else
            {

                return await exceptionList.AccountIsVerified();

            }    


        }
    }
}