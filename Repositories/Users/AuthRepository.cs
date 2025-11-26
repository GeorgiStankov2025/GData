using GData.Data;
using GData.DTOs;
using GData.Entity;
using GData.Enums;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using MimeKit;

namespace GData.Repositories.Users
{
    public class AuthRepository(GDataDbContext dbContext) : IAuthRepository
    {
        public Task<User> ChangePassword(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(RegisterUserDTO request)
        {

            var user = new User()
            {

                Username = request.Username,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,

            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.UserRole = UserRole.User;
            user.VerificationCode = Random.Shared.Next(100000, 999999);
            user.DateCreated=DateTime.UtcNow;

            SendEmailRegistration(user);

            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;

        }

        public Task<User> VerifyEmail()
        {
            throw new NotImplementedException();
        }

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

    }
}
