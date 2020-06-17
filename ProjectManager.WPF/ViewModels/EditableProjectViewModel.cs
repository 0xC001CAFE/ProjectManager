using ProjectManager.Domain.Models;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Repositories;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class EditableProjectViewModel : ViewModelBase
    {
        private readonly IProjectRepository projectRepository;

        #region Properties for data binding

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

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;

                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime endDate;
        public DateTime EndDate
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

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        #endregion

        public EditableProjectViewModel(IMessenger messenger,
                                        IViewModelLocator viewModelLocator,
                                        IProjectRepository projectRepository) : base(messenger, viewModelLocator)
        {
            this.projectRepository = projectRepository;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(SendNavigateMessage);
        }

        private void SendNavigateMessage()
        {
            messenger.Send(new NavigateMessage<ProjectViewModel>(viewModelLocator.ProjectViewModel()));
        }

        private async void Save()
        {
            var project = new Project
            {
                Name = name,
                DateRange = new DateRange
                {
                    StartDate = startDate,
                    EndDate = endDate
                },
                Description = description
            };

            await projectRepository.Add(project);

            SendNavigateMessage();
        }
    }
}
