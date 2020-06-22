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
            }, () => savedTask != null);

            #endregion

            #region Messenger

            messenger.Subscribe<SelectionChangedMessage<ProjectTaskModel>>(message =>
            {
                Load(message.SelectedElement);
            });

            #endregion
        }

        private void Load(ProjectTaskModel projectTask = null)
        {
            savedTask = projectTask;

            CurrentState = EditableTaskViewModelState.DisplayOnly;

            EditableTask = new ProjectTaskModel
            {
                StartDate = projectTask?.StartDate,
                EndDate = projectTask?.EndDate,
                Name = projectTask?.Name,
                Description = projectTask?.Description
            };
        }

        private async Task Save()
        {
            // save project task
        }
    }
}
