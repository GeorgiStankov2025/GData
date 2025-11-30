using GData.Data;
using GData.DTOs.UserDTO;
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
        public async Task<User> ChangePassword(string password,User user)
        {

            PasswordHasher<User> passwordHasher= new PasswordHasher<User>();

            user.PasswordHash = passwordHasher.HashPassword(user, password);

            user.DateModified = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return user;

        }

        public async Task<List<User>> GetAllUsers()
        {

            return await dbContext.Users.ToListAsync();

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

        public async Task<User> ResendVerificationCode(User user)
        {

            user.VerificationCode = Random.Shared.Next(100000, 999999);
            user.DateModified = DateTime.UtcNow;
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
                    user.VerificationCode = 0;
                    user.DateModified = DateTime.UtcNow;
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
