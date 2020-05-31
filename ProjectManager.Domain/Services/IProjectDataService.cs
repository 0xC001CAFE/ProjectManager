using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Services
{
    public interface IProjectDataService : IDataService<Project>
    {
        Task<IEnumerable<Project>> GetAllByUserAccount(UserAccount userAccount);

        Task<Project> CreateByUserAccount(UserAccount userAccount, Project project);
    }
}
