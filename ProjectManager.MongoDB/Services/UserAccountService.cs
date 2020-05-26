using MongoDB.Driver;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.MongoDB.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMongoCollection<UserAccount> userAccountCollection;

        public UserAccountService(IDatabaseAccess databaseAccess)
        {
            userAccountCollection = databaseAccess.GetCollection<UserAccount>("Users");
        }

        public async Task<UserAccount> Create(UserAccount userAccount)
        {
            await userAccountCollection.InsertOneAsync(userAccount);

            return userAccount;
        }

        public Task<bool> DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccount> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccount> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccount> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccount> UpdateById(string id, UserAccount userAccount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UsernameExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}
