using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using ProjectManager.MongoDB;
using ProjectManager.MongoDB.Services;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class MainAppViewModel : ViewModelBase
    {
        #region Properties for data binding

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            private set
            {
                currentViewModel = value;

                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public NotifyTaskCompletion<IEnumerable<Project>> Projects { get; private set; }

        private Project selectedProject;
        public Project SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;

                messenger.Send(new PropertyChangedMessage<Project>(selectedProject));
            }
        }

        private ProjectTask editableTask;
        public ProjectTask EditableTask
        {
            get => editableTask;
            private set
            {
                editableTask = value;

                OnPropertyChanged(nameof(EditableTask));
            }
        }

        public ICommand CreateProjectCommand { get; private set; }

        #endregion

        public MainAppViewModel(IMessenger messenger,
                                IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            CurrentViewModel = viewModelLocator.ProjectViewModel();

            CreateProjectCommand = new RelayCommand(() =>
            {
                CurrentViewModel = viewModelLocator.EditableProjectViewModel();
            });

            messenger.Subscribe<PropertyChangedMessage<ProjectTask>>(message => EditableTask = message.PropertyValue);
            messenger.Subscribe<NavigateMessage<ProjectViewModel>>(message => CurrentViewModel = message.ViewModel);

            // for debugging purposes only

            IDatabaseAccess databaseAccess = new DatabaseAccess("ProjectManager");
            IProjectDataService projectDataService = new ProjectDataService(databaseAccess);

            var userAccount = new UserAccount
            {
                Id = "5ed13657a50aaa09184bdab9"
            };

            Projects = new NotifyTaskCompletion<IEnumerable<Project>>(projectDataService.GetAllByUserAccount(userAccount));
        }
    }
}
