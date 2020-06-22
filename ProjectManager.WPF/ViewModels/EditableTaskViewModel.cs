using ProjectManager.Domain.Models;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.Repositories;
using ProjectManager.WPF.ViewModels.Locator;
using ProjectManager.WPF.ViewModels.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class EditableTaskViewModel : ViewModelBase
    {
        private readonly IProjectRepository projectRepository;
        private ProjectModel selectedProject;
        private ProjectTaskModel savedTask;

        #region Properties for data binding

        private EditableTaskViewModelState currentState;
        public EditableTaskViewModelState CurrentState
        {
            get => currentState;
            private set
            {
                currentState = value;

                OnPropertyChanged(nameof(CurrentState));
            }
        }

        private ProjectTaskModel editableTask;
        public ProjectTaskModel EditableTask
        {
            get => editableTask;
            private set
            {
                editableTask = value;

                OnPropertyChanged(nameof(EditableTask));
            }
        }

        public ICommand SaveCommand { get; }

        public ICommand EditCancelCommand { get; }

        #endregion

        public EditableTaskViewModel(IMessenger messenger,
                                     IViewModelLocator viewModelLocator,
                                     IProjectRepository projectRepository) : base(messenger, viewModelLocator)
        {
            this.projectRepository = projectRepository;

            #region Commands

            SaveCommand = new AsyncCommand(Save, () =>
            {
                return currentState != EditableTaskViewModelState.DisplayOnly;
            });

            EditCancelCommand = new RelayCommand(() =>
            {
                switch (currentState)
                {
                    case EditableTaskViewModelState.DisplayOnly:
                        CurrentState = EditableTaskViewModelState.Edit;

                        break;
                    case EditableTaskViewModelState.Edit:
                    case EditableTaskViewModelState.CreateNew:
                        CurrentState = EditableTaskViewModelState.DisplayOnly;

                        Load(savedTask);

                        break;
                }
            }, () =>
            {
                return savedTask != null || currentState == EditableTaskViewModelState.CreateNew;
            });

            #endregion

            #region Messenger

            messenger.Subscribe<SelectionChangedMessage<ProjectModel>>(message =>
            {
                selectedProject = message.SelectedElement;

                Load();
            });

            messenger.Subscribe<SelectionChangedMessage<ProjectTaskModel>>(message =>
            {
                Load(message.SelectedElement);
            });

            messenger.Subscribe<ChangeStateMessage<EditableTaskViewModelState>>(message =>
            {
                Load(null, message.NewState);
            });

            #endregion
        }

        private void Load(ProjectTaskModel projectTask = null,
                          EditableTaskViewModelState state = EditableTaskViewModelState.DisplayOnly)
        {
            savedTask = projectTask;

            CurrentState = state;

            EditableTask = new ProjectTaskModel
            {
                StartDate = projectTask?.StartDate,
                EndDate = projectTask?.EndDate,
                Name = projectTask?.Name,
                Description = projectTask?.Description
            };
        }

        private async Task<ProjectTaskModel> Save()
        {
            // TODO: Implement update function

            return await projectRepository.Add(selectedProject, editableTask);
        }
    }
}
