using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Domain.Services.Authentication
{
    public enum RegistrationResult
    {
        Success,
        UsernameAlreadyExists,
        EmailAlreadyExists,
        PasswordsDoNotMatch
    }
}
