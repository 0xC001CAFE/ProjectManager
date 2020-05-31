using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Domain.Models
{
    public class Project : ProjectComponent
    {
        public string OwnerId { get; set; }
        public IEnumerable<ProjectTask> Tasks { get; set; }
    }
}
