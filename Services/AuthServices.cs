using GData.DTOs;
using GData.Entity;
using GData.Repositories.Users;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;




namespace GData.Services
{
    public class AuthServices(IAuthRepository authRepository, IConfiguration configuration) : IAuthServices
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
            try
            {
                if (request != null && request.Username != string.Empty && request.Password != string.Empty &&
                request.Firstname != string.Empty && request.Lastname != string.Empty &&
                request.Email != string.Empty && request.Username.Length >= 8 && request.Password.Length >= 8 && request.Firstname.Length >= 2 && request.Lastname.Length >= 2)
                {

                    _ = new System.Net.Mail.MailAddress(request.Email);

                    var result = await authRepository.Register(request);
                    SendEmailRegistration(result);
                    return result;

                }
                else
                {
                    return null;
                }
            }
            catch (FormatException ex)
            {

                return null;

            }
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

            var result = await authRepository.GetUserByUsername(username);
            if (result is not null)
            {

                return result;

            }
            else
            {

                return null;

            }

        }

        public async Task<User> GetUserByIdService(Guid Id)
        {

            var result = await authRepository.GetUserById(Id);
            if (result is not null)
            {

                return result;

            }
            else
            {

                return null;

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

           return new TokenDTO { AccessToken = CreateJWTToken(user) };
            
        }

        public async Task<TokenDTO> LoginService(LoginUserDTO request)
        {

            var user = await authRepository.GetUserByUsername(request.Username);

            var passwordHasher = new PasswordHasher<User>();

            if (user is null)
            {

                return null;

            }
            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {

                return null;

            }

            return await CreateJwtToken(user);

        }

        public async Task<User> ChangePasswordService(ChangePasswordDTO request)
        {
            
            var user=await authRepository.GetUserByUsername(request.Username);

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            if(request.Username is null || request.Username==string.Empty)
            {

                return null;

            }

            if (request.Password is null || request.Password == string.Empty)
            {

                return null;

            }

            if (request.NewPassword is null || request.NewPassword == string.Empty)
            {

                return null;

            }

            if (request.NewPassword.Length<8)
            {

                return null;

            }

            if (request.Email is null || request.Email == string.Empty)
            {

                return null;
            
            }

            if(user is null)
            {

                return null;

            }

            if(request.Username!=user.Username|| request.Email!=user.Email|| passwordHasher.VerifyHashedPassword(user,user.PasswordHash,request.Password )!= PasswordVerificationResult.Success)
            {

                return null;

            }

            if (request.Username != user.Username && request.Email != user.Email && passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
            {

                return null;

            }

            else if(passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.NewPassword) == PasswordVerificationResult.Success)
            {

                return null;

            }
            else
            {

                await authRepository.ChangePassword(request.NewPassword, user);

                return user;

            }    

        }
    }
}