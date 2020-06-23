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
using ProjectManager.WPF.ViewModels.States;
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

        private ViewModelBase currentMainViewModel;
        public ViewModelBase CurrentMainViewModel
        {
            get => currentMainViewModel;
            private set
            {
                currentMainViewModel = value;

                OnPropertyChanged(nameof(CurrentMainViewModel));
            }
        }

        private ViewModelBase currentSideViewModel;
        public ViewModelBase CurrentSideViewModel
        {
            get => currentSideViewModel;
            private set
            {
                currentSideViewModel = value;

                OnPropertyChanged(nameof(CurrentSideViewModel));
            }
        }

        public ObservableCollection<ProjectModel> Projects => projectRepository.Projects;

        private ProjectModel selectedProject;
        public ProjectModel SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;

                OnPropertyChanged(nameof(SelectedProject));

                messenger.Send(new SelectionChangedMessage<ProjectModel>(selectedProject));

                if (currentMainViewModel.GetType() != typeof(ProjectViewModel))
                {
                    CurrentMainViewModel = viewModelLocator.ProjectViewModel();
                }
            }
        }

        public ICommand EditProjectCommand { get; }

        public ICommand NewProjectCommand { get; }

        #endregion

        public MainAppViewModel(IMessenger messenger,
                                IViewModelLocator viewModelLocator,
                                IProjectRepository projectRepository) : base(messenger, viewModelLocator)
        {
            this.projectRepository = projectRepository;

            CurrentMainViewModel = viewModelLocator.ProjectViewModel();
            CurrentSideViewModel = viewModelLocator.EditableTaskViewModel();

            WeakEventManager<IProjectRepository, ProjectsLoadedEventArgs>.AddHandler(projectRepository, nameof(projectRepository.ProjectsLoaded), (sender, eventArgs) =>
            {
                if (eventArgs.LoadedSuccessfully) OnPropertyChanged(nameof(Projects));
                else messenger.Send(new ExceptionOccurredMessage(new Exception("Fehler beim Laden der Projekte. Dieser Fehler tritt auf, wenn keine Verbindung zur Datenbank aufgebaut werden konnte.", eventArgs.Exception)));
            });

            #region Commands

            EditProjectCommand = new RelayCommand(() =>
            {
                var viewModel = viewModelLocator.EditableProjectViewModel();

                viewModel.Load(selectedProject);
                CurrentMainViewModel = viewModel;

                messenger.Send(new ChangeStateMessage<EditableTaskViewModelState>(EditableTaskViewModelState.DisplayOnly));
            }, () =>
            {
                return selectedProject != null && currentMainViewModel.GetType() != typeof(EditableProjectViewModel);
            });

            NewProjectCommand = new RelayCommand(() =>
            {
                var viewModel = viewModelLocator.EditableProjectViewModel();

                viewModel.Load();
                CurrentMainViewModel = viewModel;

                messenger.Send(new ChangeStateMessage<EditableTaskViewModelState>(EditableTaskViewModelState.DisplayOnly));
            });

            #endregion

            #region Messenger

            messenger.Subscribe<NavigateMessage>(message =>
            {
                if (message.ViewModel == typeof(ProjectViewModel)) CurrentMainViewModel = viewModelLocator.ProjectViewModel();
            });

            messenger.Subscribe<ChangeSelectionMessage<ProjectModel>>(message =>
            {
                SelectedProject = message.NewSelectedElement;
            });

            #endregion
        }
    }
}
