using ProjectManager.Domain.Models;
using ProjectManager.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.WPF.Repositories
{
    public interface IProjectRepository
    {
        ObservableCollection<ProjectModel> Projects { get; }

        public event EventHandler<ProjectsLoadedEventArgs> ProjectsLoaded;

        void Load();

        Task<ProjectModel> Add(ProjectModel project);

        Task<ProjectTaskModel> Add(ProjectModel project, ProjectTaskModel projectTask);

        Task<ProjectModel> Update(ProjectModel project);

        Task<ProjectTaskModel> Update(ProjectModel project, ProjectTaskModel projectTask);
    }
}
