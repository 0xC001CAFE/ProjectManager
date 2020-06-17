using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using ProjectManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.WPF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IProjectDataService projectDataService;

        // for debugging purposes only
        private readonly UserAccount userAccount = new UserAccount { Id = "5ed13657a50aaa09184bdab9" };

        public NotifyTaskCompletion<ObservableCollection<Project>> Projects { get; private set; }

        public ProjectRepository(IProjectDataService projectDataService)
        {
            this.projectDataService = projectDataService;

            Load();
        }

        public void Load()
        {
            var task = projectDataService.GetAllByUserAccount(userAccount).ContinueWith(task =>
            {
                return new ObservableCollection<Project>(task.Result);
            });

            Projects = new NotifyTaskCompletion<ObservableCollection<Project>>(task);
        }

        public async Task Add(Project project)
        {
            var storedProject = await projectDataService.CreateByUserAccount(userAccount, project);

            Projects.Result.Add(storedProject);
        }
    }
}
