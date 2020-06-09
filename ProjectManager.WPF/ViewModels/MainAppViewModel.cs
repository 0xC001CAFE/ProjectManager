using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class MainAppViewModel : ViewModelBase
    {
        private Project selectedProject;

        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;

                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        public List<Project> Projects { get; private set; } = new List<Project>();

        public MainAppViewModel()
        {
            Projects.Add(new Project
            {
                Name = "Projekt 1",
                Description = "Beschreibung des Projekts 1",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                }
            });

            Projects.Add(new Project
            {
                Name = "Projekt 2",
                Description = "Beschreibung des Projekts 2",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                }
            });

            Projects.Add(new Project
            {
                Name = "Projekt 3",
                Description = "Beschreibung des Projekts 3",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                }
            });

            Projects.Add(new Project
            {
                Name = "Projekt 4",
                Description = "Beschreibung des Projekts 4",
                DateRange = new DateRange
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                }
            });
        }
    }
}
