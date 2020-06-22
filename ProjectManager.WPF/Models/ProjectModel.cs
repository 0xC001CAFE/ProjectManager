using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjectManager.WPF.Models
{
    public class ProjectModel : ProjectComponentModel, IDataErrorInfo
    {
        public ObservableCollection<ProjectTaskModel> Tasks { get; private set; }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        public ProjectModel() { }

        public ProjectModel(in Project project) => Map(project);

        public void Map(in Project project)
        {
            base.Map(project);

            if (project.Tasks == null) return;

            Tasks = new ObservableCollection<ProjectTaskModel>();

            foreach (var projectTask in project.Tasks)
            {
                Tasks.Add(new ProjectTaskModel(projectTask));
            }
        }

        public void MapBack(Project project) => base.MapBack(project);
    }
}
