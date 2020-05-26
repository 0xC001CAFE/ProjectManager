using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Domain.Models
{
    public class Project : ProjectComponent
    {
        public UserAccount Owner { get; set; }
        public IEnumerable<ProjectTask> Tasks { get; set; }
    }
}
