using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class MainViewModel
    {
        public List<Project> Projects { get; private set; } = new List<Project>();

        public MainViewModel()
        {
            Projects.Add(new Project
            {
                Name = "Projekt 1",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7d)
                },
                Description = "Beschreibung des Projekts 1"
            });

            Projects.Add(new Project
            {
                Name = "Projekt 2",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7d)
                },
                Description = "Beschreibung des Projekts 2"
            });

            Projects.Add(new Project
            {
                Name = "Projekt 3",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7d)
                },
                Description = "Beschreibung des Projekts 3"
            });
        }
    }
}
