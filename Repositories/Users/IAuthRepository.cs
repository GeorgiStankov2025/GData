using GData.DTOs;
using GData.Entity;

namespace GData.Repositories.Users
{
    public interface IAuthRepository
    {
        public Task<User> Register(RegisterUserDTO request);
        public Task<User> Logout();
        public Task<User> ChangePassword(User user);
        public Task<bool> VerifyAccount(User user, int code);
        public Task<User> GetUserById(Guid Id);
        public Task<User> GetUserByUsername(string username);

    }
}
