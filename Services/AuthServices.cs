using GData.DTOs;
using GData.Entity;
using GData.Repositories.Users;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;




namespace GData.Services
{
    public class AuthServices(IAuthRepository authRepository) : IAuthServices
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
            
          if (request != null&& request.Username!=string.Empty&&request.Password!=string.Empty&&
          request.Firstname!=string.Empty&&request.Lastname!=string.Empty&&
          request.Email!=string.Empty&&request.Username.Length>=8&&request.Password.Length>=8&&request.Firstname.Length>=2&&request.Lastname.Length>=2)
          {
            
              try
              {

                 _ = new System.Net.Mail.MailAddress(request.Email);

                 var result = await authRepository.Register(request);
                 if (result is not null)
                 {  
                        
                     SendEmailRegistration(result);
                     return result;

                 }
                 else return null;

              }
              catch (System.FormatException)
              {

                    return null;

              }
                
          }

          else
          {

            return null;

          }
        
        }
    }
}
