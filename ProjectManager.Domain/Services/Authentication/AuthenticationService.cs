using ProjectManager.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserAccountDataService accountData;

        public AuthenticationService(IUserAccountDataService userAccountDataService)
        {
            accountData = userAccountDataService;
        }

        public async Task<UserAccount> LoginByEmail(string email, string password)
        {
            var userAccount = await accountData.GetByEmail(email);

            return LoginByUserAccount(userAccount, password);
        }

        public async Task<UserAccount> LoginByUsername(string username, string password)
        {
            var userAccount = await accountData.GetByUsername(username);

            return LoginByUserAccount(userAccount, password);
        }

        public async Task<RegistrationResult> Register(string username, string email, string password, string confirmedPassword)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new NotImplementedException();

            if (string.IsNullOrWhiteSpace(email)) throw new NotImplementedException();

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmedPassword)) throw new NotImplementedException();

            if (password != confirmedPassword) return RegistrationResult.PasswordsDoNotMatch;

            if (await accountData.UsernameExists(username)) return RegistrationResult.UsernameAlreadyExists;

            if (await accountData.EmailExists(email)) return RegistrationResult.EmailAlreadyExists;

            var userAccount = new UserAccount
            {
                Username = username,
                Email = email,
                Password = password
            };

            await accountData.Create(userAccount);

            return RegistrationResult.Success;
        }

        private UserAccount LoginByUserAccount(UserAccount userAccount, string password)
        {
            if (userAccount == null) throw new NotImplementedException();

            if (password != userAccount.Password) throw new NotImplementedException();

            return userAccount;
        }
    }
}
