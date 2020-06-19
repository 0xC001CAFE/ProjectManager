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
        NotifyTaskCompletion<ObservableCollection<Project>> Projects { get; }

        void Load();

        Task Add(Project project);

        Task Update(Project project);
    }
}
