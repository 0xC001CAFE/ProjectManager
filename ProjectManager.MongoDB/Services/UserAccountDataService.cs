using MongoDB.Driver;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.MongoDB.Services
{
    public class UserAccountDataService : IUserAccountDataService
    {
        private readonly IMongoCollection<UserAccount> accountCollection;

        public UserAccountDataService(IDatabaseAccess databaseAccess)
        {
            accountCollection = databaseAccess.GetCollection<UserAccount>("Users");
        }

        public async Task<UserAccount> Create(UserAccount userAccount)
        {
            await accountCollection.InsertOneAsync(userAccount);

            return userAccount;
        }

        public async Task<UserAccount> DeleteById(string id)
        {
            return await accountCollection.FindOneAndDeleteAsync(account => account.Id == id);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await GetByEmail(email) != null;
        }

        public async Task<UserAccount> GetByEmail(string email)
        {
            return await accountCollection.Find(account => account.Email == email).SingleOrDefaultAsync();
        }

        public async Task<UserAccount> GetById(string id)
        {
            return await accountCollection.Find(account => account.Id == id).SingleOrDefaultAsync();
        }

        public async Task<UserAccount> GetByUsername(string username)
        {
            return await accountCollection.Find(account => account.Username == username).SingleOrDefaultAsync();
        }

        public async Task<UserAccount> UpdateById(string id, UserAccount userAccount)
        {
            if (userAccount.Id == null) userAccount.Id = id;

            return await accountCollection.FindOneAndReplaceAsync(account => account.Id == id, userAccount);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await GetByUsername(username) != null;
        }
    }
}
