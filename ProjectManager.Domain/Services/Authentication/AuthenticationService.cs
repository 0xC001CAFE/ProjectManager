using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserAccountService userAccountService;

        public AuthenticationService(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        public async Task<UserAccount> LoginByEmail(string email, string password)
        {
            var userAccount = await userAccountService.GetByEmail(email);

            return LoginByUserAccount(userAccount, password);
        }

        public async Task<UserAccount> LoginByUsername(string username, string password)
        {
            var userAccount = await userAccountService.GetByUsername(username);

            return LoginByUserAccount(userAccount, password);
        }

        public async Task<RegistrationResult> Register(string username, string email, string password, string confirmedPassword)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new NotImplementedException();

            if (string.IsNullOrWhiteSpace(email)) throw new NotImplementedException();

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmedPassword)) throw new NotImplementedException();

            if (password != confirmedPassword) return RegistrationResult.PasswordsDoNotMatch;

            if (await userAccountService.UsernameExists(username)) return RegistrationResult.UsernameAlreadyExists;

            if (await userAccountService.EmailExists(email)) return RegistrationResult.EmailAlreadyExists;

            var userAccount = new UserAccount
            {
                Username = username,
                Email = email,
                Password = password
            };

            await userAccountService.Create(userAccount);

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
