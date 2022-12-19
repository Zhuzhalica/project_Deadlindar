using System.Threading.Tasks;

namespace WebAPI.Server.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RegistrationResponse> Register(RegisterRequest request);
    }
}