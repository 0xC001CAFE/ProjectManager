using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels.Locator
{
    public interface IViewModelLocator
    {
        ViewModelFactory<MainAppViewModel> MainAppViewModel { get; }

        ViewModelFactory<ProjectViewModel> ProjectViewModel { get; }

        ViewModelFactory<EditableProjectViewModel> EditableProjectViewModel { get; }

        ViewModelFactory<EditableTaskViewModel> EditableTaskViewModel { get; }
    }
}
