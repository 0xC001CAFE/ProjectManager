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

        public ObservableCollection<ProjectModel> Projects { get; private set; }

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

                Projects = new ObservableCollection<ProjectModel>();

                // map each project and add it to the list

                if (projects == null) return;

                foreach (var project in projects)
                {
                    var mappedProject = new ProjectModel(project);

                    Projects.Add(mappedProject);

                    // map tasks of the project

                    if (project.Tasks == null) continue;

                    foreach (var projectTask in project.Tasks)
                    {
                        mappedProject.Tasks.Add(new ProjectTaskModel(projectTask));
                    }
                }
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

        public async Task<ProjectModel> Add(ProjectModel project)
        {
            var mappedProject = new Project();
            project.MapBack(mappedProject);

            var storedProject = await projectDataService.CreateByUserAccount(userAccount, mappedProject);
            project.Map(storedProject);

            Projects.Add(project);

            return project;
        }

        public async Task<ProjectModel> Update(ProjectModel project)
        {
            return project;
        }
    }
}
