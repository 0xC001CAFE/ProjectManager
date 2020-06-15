﻿using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using ProjectManager.MongoDB;
using ProjectManager.MongoDB.Services;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class MainAppViewModel : ViewModelBase
    {
        #region Properties for data binding

        public ViewModelBase CurrentViewModel { get; private set; }

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

        #endregion

        public MainAppViewModel(IMessenger messenger,
                                IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            messenger.Subscribe<PropertyChangedMessage<ProjectTask>>(message => EditableTask = message.PropertyValue);

            CurrentViewModel = viewModelLocator.ProjectViewModel();

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
