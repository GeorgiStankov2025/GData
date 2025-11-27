using GData.Data;
using GData.DTOs;
using GData.Entity;
using GData.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GData.Repositories.Users
{
    public class AuthRepository(GDataDbContext dbContext) : IAuthRepository
    {
        public Task<User> ChangePassword(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(Guid Id)
        {

            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);

        }

        public async Task<User> GetUserByUsername(string username)
        {

            return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            
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

            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;

        }

        public async Task<bool> VerifyAccount(User user,int code)
        {

            if(user!=null)
            {

                if (code == user.VerificationCode)
                {

                    user.IsEmailConfirmed = true;
                    await dbContext.SaveChangesAsync();
                    return true;
                }

                else
                {

                    return false;

                }

            }
            else
            {

                return false;

            }

        }

    }
}
