using MongoDB.Driver;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using System.Threading.Tasks;

namespace ProjectManager.MongoDB.Services
{
    public class UserAccountDataService : DataService<UserAccount>, IUserAccountDataService
    {
        public UserAccountDataService(IDatabaseAccess databaseAccess) : base(databaseAccess.GetCollection<UserAccount>("Users")) { }

        public async Task<bool> EmailExists(string email)
        {
            return await GetByEmail(email) != null;
        }

        public async Task<UserAccount> GetByEmail(string email)
        {
            return await modelCollection.Find(account => account.Email == email).SingleOrDefaultAsync();
        }

        public async Task<UserAccount> GetByUsername(string username)
        {
            return await modelCollection.Find(account => account.Username == username).SingleOrDefaultAsync();
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await GetByUsername(username) != null;
        }
    }
}
