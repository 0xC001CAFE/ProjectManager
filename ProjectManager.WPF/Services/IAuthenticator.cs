using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Services
{
    public interface IAuthenticator
    {
        UserAccount CurrentUser { get; }
    }
}
