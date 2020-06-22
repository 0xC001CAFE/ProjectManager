using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjectManager.WPF.Models
{
    public class ProjectTaskModel : ProjectComponentModel, IDataErrorInfo
    {
        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        public ProjectTaskModel() { }

        public ProjectTaskModel(in ProjectTask projectTask) => Map(projectTask);

        public void Map(in ProjectTask projectTask) => base.Map(projectTask);

        public void MapBack(ProjectTask projectTask) => base.MapBack(projectTask);
    }
}
