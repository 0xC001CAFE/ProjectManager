using ProjectManager.Domain.Models;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserAccount> LoginByUsername(string username, string password);

        Task<UserAccount> LoginByEmail(string email, string password);

        Task<RegistrationResult> Register(string username, string email, string password, string confirmedPassword);
    }
}
