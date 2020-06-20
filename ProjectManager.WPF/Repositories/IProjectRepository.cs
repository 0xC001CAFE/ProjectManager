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
        ObservableCollection<Project> Projects { get; }

        public event EventHandler<ProjectsLoadedEventArgs> ProjectsLoaded;

        void Load();

        Task Add(Project project);

        Task Update(Project project);
    }
}
