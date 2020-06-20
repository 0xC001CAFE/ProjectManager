using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using ProjectManager.MongoDB;
using ProjectManager.MongoDB.Services;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.Repositories;
using ProjectManager.WPF.ViewModels.Locator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class MainAppViewModel : ViewModelBase
    {
        private readonly IProjectRepository projectRepository;

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

        public ObservableCollection<Project> Projects => projectRepository.Projects;

        private Project selectedProject;
        public Project SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;

                messenger.Send(new SelectionChangedMessage<Project>(selectedProject));
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

        public ICommand EditProjectCommand { get; private set; }

        public ICommand NewProjectCommand { get; private set; }

        #endregion

        public MainAppViewModel(IMessenger messenger,
                                IViewModelLocator viewModelLocator,
                                IProjectRepository projectRepository) : base(messenger, viewModelLocator)
        {
            this.projectRepository = projectRepository;

            CurrentViewModel = viewModelLocator.ProjectViewModel();

            WeakEventManager<IProjectRepository, ProjectsLoadedEventArgs>.AddHandler(projectRepository, nameof(projectRepository.ProjectsLoaded), (sender, eventArgs) =>
            {
                if (eventArgs.LoadedSuccessfully) OnPropertyChanged(nameof(Projects));
            });

            #region Commands

            EditProjectCommand = new RelayCommand(() =>
            {
                var viewModel = viewModelLocator.EditableProjectViewModel();

                viewModel.Load(selectedProject);
                CurrentViewModel = viewModel;
            }, () => selectedProject != null);

            NewProjectCommand = new RelayCommand(() =>
            {
                var viewModel = viewModelLocator.EditableProjectViewModel();

                viewModel.Load();
                CurrentViewModel = viewModel;
            });

            #endregion

            #region Messenger

            messenger.Subscribe<NavigateMessage>(message =>
            {
                if (message.ViewModel == typeof(ProjectViewModel)) CurrentViewModel = viewModelLocator.ProjectViewModel();
            });

            #endregion
        }
    }
}
