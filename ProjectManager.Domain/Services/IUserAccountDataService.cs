using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Services
{
    public interface IUserAccountDataService : IDataService<UserAccount>
    {
        Task<UserAccount> GetByUsername(string username);

        Task<UserAccount> GetByEmail(string email);

        Task<bool> UsernameExists(string username);

        Task<bool> EmailExists(string email);
    }
}
