using GData.DTOs;
using GData.Entity;

namespace GData.Services
{
    public interface IAuthServices
    {

        public Task<User> RegisterService(RegisterUserDTO request);

    }
}
