using ProjectManager.Domain.Models;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Repositories;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class EditableProjectViewModel : ViewModelBase
    {
        private readonly IProjectRepository projectRepository;
        private Project editableProject;

        #region Properties for data binding

        private bool editMode;
        public bool EditMode
        {
            get => editMode;
            private set
            {
                editMode = value;

                OnPropertyChanged(nameof(EditMode));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;

                OnPropertyChanged(nameof(Name));
            }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;

                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get => endDate;
            set
            {
                endDate = value;

                OnPropertyChanged(nameof(EndDate));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;

                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand SaveCommand { get; }

        public ICommand CancelCommand { get; }

        #endregion

        public EditableProjectViewModel(IMessenger messenger,
                                        IViewModelLocator viewModelLocator,
                                        IProjectRepository projectRepository) : base(messenger, viewModelLocator)
        {
            this.projectRepository = projectRepository;

            #region Commands

            SaveCommand = new AsyncCommand<Project>(Save, (success, project) =>
            {
                if (!success)
                {
                    Debug.WriteLine("An error occurred while saving the project.");

                    messenger.Send(new ExceptionOccurredMessage(new Exception("Fehler beim Speichern des Projekts. Dieser Fehler tritt auf, wenn keine Verbindung zur Datenbank aufgebaut werden konnte.")));

                    return;
                }

                if (editMode) messenger.Send(new NavigateMessage(typeof(ProjectViewModel)));
                else messenger.Send(new ChangeSelectionMessage<Project>(project));
            });

            CancelCommand = new RelayCommand(() =>
            {
                messenger.Send(new NavigateMessage(typeof(ProjectViewModel)));
            });

            #endregion
        }

        public void Load(Project project = null)
        {
            editableProject = project;

            EditMode = project != null;

            Name = project?.Name;
            StartDate = project?.DateRange.StartDate;
            EndDate = project?.DateRange.EndDate;
            Description = project?.Description;
        }

        private async Task<Project> Save()
        {
            if (editMode)
            {
                editableProject.Name = name;
                editableProject.DateRange.StartDate = startDate ?? default;
                editableProject.DateRange.EndDate = endDate ?? default;
                editableProject.Description = description;

                return await projectRepository.Update(editableProject);
            }

            var project = new Project
            {
                Name = name,
                DateRange = new DateRange
                {
                    StartDate = startDate ?? default,
                    EndDate = endDate ?? default
                },
                Description = description
            };

            return await projectRepository.Add(project);
        }
    }
}
