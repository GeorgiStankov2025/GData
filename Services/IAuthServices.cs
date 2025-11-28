using GData.DTOs;
using GData.Entity;

namespace GData.Services
{
    public interface IAuthServices
    {
        public Task<User> RegisterService(RegisterUserDTO request);
        public Task<bool> VerifyAccountService(Guid Id,int code);
        public Task<TokenDTO> LoginService(LoginUserDTO request);
        public Task<User> ChangePasswordService(ChangePasswordDTO request);
        public Task<User> GetUserByUsernameService(string username);
        public Task<User> GetUserByIdService(Guid Id);
        public Task<List<User>> GetAllUsersService();

    }

}
