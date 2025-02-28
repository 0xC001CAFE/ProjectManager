﻿using MongoDB.Bson;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjectManager.WPF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IProjectDataService projectDataService;
        private readonly IAuthenticator authenticator;

        public ObservableCollection<ProjectModel> Projects { get; private set; }

        public event EventHandler<ProjectsLoadedEventArgs> ProjectsLoaded;

        public ProjectRepository(IProjectDataService projectDataService, IAuthenticator authenticator)
        {
            this.projectDataService = projectDataService;
            this.authenticator = authenticator;

            Load();
        }

        public async void Load()
        {
            Exception occurredException = null;

            try
            {
                var projects = await projectDataService.GetAllByUserAccount(authenticator.CurrentUser);

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
            // create new project and map project model to it
            var mappedProject = new Project();
            project.MapBack(mappedProject);

            await projectDataService.CreateByUserAccount(authenticator.CurrentUser, mappedProject);

            // map project to project model to get current id
            project.Map(mappedProject);

            Projects.Add(project);

            return project;
        }

        public async Task<ProjectTaskModel> Add(ProjectModel project, ProjectTaskModel projectTask)
        {
            var storedProject = await projectDataService.GetById(project.Id);

            // create new project task with unique id
            var mappedTask = new ProjectTask
            {
                Id = ObjectId.GenerateNewId().ToString()
            };

            // map project task model to project task and add it to the list
            projectTask.MapBack(mappedTask);
            GetProjectTasks(storedProject).Add(mappedTask);

            await projectDataService.UpdateById(storedProject.Id, storedProject);

            // map project task to project task model to get current id
            projectTask.Map(mappedTask);

            project.Tasks.Add(projectTask);

            return projectTask;
        }

        public async Task<ProjectModel> Update(ProjectModel project)
        {
            var storedProject = await projectDataService.GetById(project.Id);

            // map project model to project
            project.MapBack(storedProject);

            await projectDataService.UpdateById(storedProject.Id, storedProject);

            return project;
        }

        public async Task<ProjectTaskModel> Update(ProjectModel project, ProjectTaskModel projectTask)
        {
            var storedProject = await projectDataService.GetById(project.Id);

            foreach (var storedTask in GetProjectTasks(storedProject))
            {
                if (storedTask.Id == projectTask.Id)
                {
                    // map project task model to project task
                    projectTask.MapBack(storedTask);

                    break;
                }
            }

            await projectDataService.UpdateById(storedProject.Id, storedProject);

            return projectTask;
        }

        private List<ProjectTask> GetProjectTasks(Project project)
        {
            var projectTasks = project.Tasks == null ? new List<ProjectTask>() : new List<ProjectTask>(project.Tasks);

            project.Tasks = projectTasks;

            return projectTasks;
        }
    }
}
