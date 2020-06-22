using ProjectManager.Domain.Models;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
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
        private ProjectModel savedProject;

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

        public ProjectModel EditableProject { get; private set; }

        public ICommand SaveCommand { get; }

        public ICommand CancelCommand { get; }

        #endregion

        public EditableProjectViewModel(IMessenger messenger,
                                        IViewModelLocator viewModelLocator,
                                        IProjectRepository projectRepository) : base(messenger, viewModelLocator)
        {
            this.projectRepository = projectRepository;

            #region Commands

            SaveCommand = new AsyncCommand<ProjectModel>(Save, (success, project) =>
            {
                if (!success)
                {
                    Debug.WriteLine("An error occurred while saving the project.");

                    messenger.Send(new ExceptionOccurredMessage(new Exception("Fehler beim Speichern des Projekts. Dieser Fehler tritt auf, wenn keine Verbindung zur Datenbank aufgebaut werden konnte.")));

                    return;
                }

                if (editMode) messenger.Send(new NavigateMessage(typeof(ProjectViewModel)));
                else messenger.Send(new ChangeSelectionMessage<ProjectModel>(project));
            });

            CancelCommand = new RelayCommand(() =>
            {
                messenger.Send(new NavigateMessage(typeof(ProjectViewModel)));
            });

            #endregion
        }

        public void Load(ProjectModel project = null)
        {
            savedProject = project;

            EditMode = project != null;

            EditableProject = new ProjectModel
            {
                StartDate = project?.StartDate,
                EndDate = project?.EndDate,
                Name = project?.Name,
                Description = project?.Description
            };
        }

        private async Task<ProjectModel> Save()
        {
            if (editMode)
            {
                savedProject.StartDate = EditableProject.StartDate;
                savedProject.EndDate = EditableProject.EndDate;
                savedProject.Name = EditableProject.Name;
                savedProject.Description = EditableProject.Description;

                return await projectRepository.Update(savedProject);
            }

            return await projectRepository.Add(EditableProject);
        }
    }
}
