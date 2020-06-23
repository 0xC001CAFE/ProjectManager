using ProjectManager.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProjectManager.WPF.Models
{
    public class ProjectModel : ProjectComponentModel, IDataErrorInfo
    {
        public ObservableCollection<ProjectTaskModel> Tasks { get; } = new ObservableCollection<ProjectTaskModel>();

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        public ProjectModel() { }

        public ProjectModel(in Project project) => Map(project);

        public void Map(in Project project) => base.Map(project);

        public void MapBack(Project project) => base.MapBack(project);
    }
}
