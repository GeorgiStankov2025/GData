using GData.DTOs.UserDTO;
using GData.Entity;

namespace GData.Repositories.Users
{
    public interface IAuthRepository
    {
        public Task<User> Register(RegisterUserDTO request);
        public Task<User> Logout();
        public Task<User> ChangePassword(string password, User user);
        public Task<bool> VerifyAccount(User user, int code);
        public Task<User> GetUserById(Guid Id);
        public Task<User> GetUserByUsername(string username);
        public Task<List<User>> GetAllUsers();
        public Task<User> ResendVerificationCode(User user);
        

    }
}
