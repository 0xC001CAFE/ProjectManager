using MongoDB.Driver;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.MongoDB.Services
{
    public class ProjectDataService : DataService<Project>, IProjectDataService
    {
        public ProjectDataService(IDatabaseAccess databaseAccess) : base(databaseAccess.GetCollection<Project>("Projects")) { }

        public async Task<Project> CreateByUserAccount(UserAccount userAccount, Project project)
        {
            project.OwnerId = userAccount.Id;

            return await Create(project);
        }

        public async Task<IEnumerable<Project>> GetAllByUserAccount(UserAccount userAccount)
        {
            return await modelCollection.Find(project => project.OwnerId == userAccount.Id).ToListAsync();
        }
    }
}
