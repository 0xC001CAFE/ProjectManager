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
