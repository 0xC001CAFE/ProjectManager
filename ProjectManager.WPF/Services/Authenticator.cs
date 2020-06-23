using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Services
{
    public class Authenticator : IAuthenticator
    {
        public UserAccount CurrentUser { get; private set; }

        public Authenticator()
        {
            // for debugging purposes only
            CurrentUser = new UserAccount
            {
                Id = "5ef1ef9e8227c93ef090b5db",
                Username = "Max Mustermann",
                Email = "max.mustermann@mustermail.de",
                Password = "musterpasswort"
            };
        }
    }
}
