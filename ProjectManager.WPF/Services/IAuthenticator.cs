using ProjectManager.Domain.Models;

namespace ProjectManager.WPF.Services
{
    public interface IAuthenticator
    {
        UserAccount CurrentUser { get; }
    }
}
