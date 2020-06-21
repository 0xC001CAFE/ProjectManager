using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using ProjectManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.WPF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IProjectDataService projectDataService;

        // for debugging purposes only
        private readonly UserAccount userAccount = new UserAccount { Id = "5ed13657a50aaa09184bdab9" };

        public ObservableCollection<Project> Projects { get; private set; }

        public event EventHandler<ProjectsLoadedEventArgs> ProjectsLoaded;

        public ProjectRepository(IProjectDataService projectDataService)
        {
            this.projectDataService = projectDataService;

            Load();
        }

        public async void Load()
        {
            Exception occurredException = null;

            try
            {
                var projects = await projectDataService.GetAllByUserAccount(userAccount);

                Projects = new ObservableCollection<Project>(projects);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("An error occurred while loading the projects.");

                occurredException = exception;
            }
            finally
            {
                ProjectsLoaded?.Invoke(this, new ProjectsLoadedEventArgs(occurredException));
            }
        }

        public async Task<Project> Add(Project project)
        {
            var storedProject = await projectDataService.CreateByUserAccount(userAccount, project);

            Projects.Add(storedProject);

            return storedProject;
        }

        public async Task<Project> Update(Project project)
        {
            var storedProject = await projectDataService.UpdateById(project.Id, project);

            // update local list

            return storedProject;
        }
    }
}
