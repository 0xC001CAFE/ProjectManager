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
            
            await projectDataService.CreateByUserAccount(userAccount, mappedProject);
            project.Map(mappedProject);

            Projects.Add(project);

            return project;
        }

        public async Task<ProjectTaskModel> Add(ProjectModel project, ProjectTaskModel projectTask)
        {
            var storedProject = await projectDataService.GetById(project.Id);

            var mappedTask = new ProjectTask();
            projectTask.MapBack(mappedTask);

            GetProjectTasks(storedProject).Add(mappedTask);
            await projectDataService.UpdateById(project.Id, storedProject);

            project.Tasks.Add(projectTask);

            return projectTask;
        }

        public async Task<ProjectModel> Update(ProjectModel project)
        {
            var storedProject = await projectDataService.GetById(project.Id);

            project.MapBack(storedProject);
            await projectDataService.UpdateById(storedProject.Id, storedProject);

            return project;
        }

        private List<ProjectTask> GetProjectTasks(Project project)
        {
            var projectTasks = project.Tasks == null ? new List<ProjectTask>() : new List<ProjectTask>(project.Tasks);

            project.Tasks = projectTasks;

            return projectTasks;
        }
    }
}
