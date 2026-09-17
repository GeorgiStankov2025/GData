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
using System.Security.Claims;
using System.Text;

namespace GData.Services.Users;

public class AuthServices(IAuthRepository authRepository, IConfiguration configuration) : IAuthServices
{
    private async Task SendEmailRegistration(User user)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("bitproductions2024@gmail.com"));

        try
        {
            email.To.Add(MailboxAddress.Parse(user.Email));
        }
        catch (FormatException)
        {
            throw new BadRequestException("Recipient email address format is invalid.");
        }

        email.Subject = "GData Registration";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = $"Hi, {user.Username}! Welcome to GData. You have successfully registered to our website. You can activate your account using the code: {user.VerificationCode} \n\n\n GData Team"
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
        await smtp.AuthenticateAsync("bitproductions2024@gmail.com", "unbv xvlo wrvs vgnm");
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task<User> RegisterService(RegisterUserDTO request)
    {
        if (request == null)
            throw new BadRequestException("Error processing registration request.");

        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Username))
            throw new BadRequestException("Please fill all required fields.");

        if (!System.Net.Mail.MailAddress.TryCreate(request.Email, out _))
            throw new BadRequestException("Invalid email format.");

        if (await authRepository.UsernameExistsAsync(request.Username))
            throw new BadRequestException("Username already exists.");

        if (await authRepository.EmailExistsAsync(request.Email))
            throw new BadRequestException("Email already exists.");

        var result = await authRepository.Register(request);

        await SendEmailRegistration(result);
        return result;
    }

    public async Task<bool> VerifyAccountService(Guid Id, int code)
    {
        var user = await authRepository.GetUserById(Id);

        if (user != null)
        {
            var result = await authRepository.VerifyAccount(user, code);
            return result is true;
        }

        return false;
    }

    public async Task<User> GetUserByUsernameService(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new BadRequestException("No username provided in request.");
        }

        var result = await authRepository.GetUserByUsername(username);

        if (result is null)
        {
            throw new NotFoundException("User not found with specified username.");
        }

        return result;
    }

    public async Task<User> GetUserByIdService(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new BadRequestException("No ID provided in request.");
        }

        var result = await authRepository.GetUserById(Id);

        if (result is null)
        {
            throw new NotFoundException("User not found with specified ID.");
        }

        return result;
    }

    private string CreateJWTToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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
            throw new NotFoundException("User does not exist.");
        }

        if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid login credentials.");
        }

        return await CreateJwtToken(user);
    }

    public async Task<User> ChangePasswordService(Guid Id, ChangePasswordDTO request)
    {
        var user = await authRepository.GetUserByUsername(request.Username);

        if (user is null)
        {
            throw new NotFoundException("Error processing password change request.");
        }

        if (Id != user.Id)
        {
            throw new ForbiddenException("Invalid user ID for this request.");
        }

        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.Email))
        {
            throw new BadRequestException("Please fill all boxes to change password.");
        }

        if (request.NewPassword.Length < 8)
        {
            throw new BadRequestException("Invalid new password length.");
        }

        if (request.Username != user.Username || request.Email != user.Email || passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
        {
            throw new BadRequestException("Invalid user credentials.");
        }

        if (request.Username != user.Username && request.Email != user.Email && passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
        {
            throw new BadRequestException("Invalid user credentials.");
        }

        if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.NewPassword) == PasswordVerificationResult.Success)
        {
            throw new BadRequestException("New password cannot be the same as old password.");
        }

        if (user.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Email is not verified.");
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
        var user = await GetUserByIdService(Id);

        if (user.IsEmailConfirmed == false)
        {
            var result = await authRepository.ResendVerificationCode(user);
            SendEmailRegistration(user);
            return result;
        }

        throw new BadRequestException("Account is already verified.");
    }
}