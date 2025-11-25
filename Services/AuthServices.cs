using GData.DTOs;
using GData.Entity;
using GData.Repositories.Users;

namespace GData.Services
{
    public class AuthServices(IAuthRepository authRepository) : IAuthServices
    {
        public async Task<User> RegisterService(RegisterUserDTO request)
        {
            
          if (request != null)
          {

            return await authRepository.Register(request);

          }

          else
          {

            return null;

          }
        
        }
    }
}
