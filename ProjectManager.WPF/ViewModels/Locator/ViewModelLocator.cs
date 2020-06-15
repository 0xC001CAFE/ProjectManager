using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels.Locator
{
    public class ViewModelLocator : IViewModelLocator
    {
        public ViewModelFactory<MainAppViewModel> MainAppViewModel { get; private set; }

        public ViewModelFactory<ProjectViewModel> ProjectViewModel { get; private set; }

        public ViewModelFactory<EditableProjectViewModel> EditableProjectViewModel { get; private set; }

        public ViewModelLocator(ViewModelFactory<MainAppViewModel> mainAppViewModel,
                                ViewModelFactory<ProjectViewModel> projectViewModel,
                                ViewModelFactory<EditableProjectViewModel> editableProjectViewModel)
        {
            MainAppViewModel = mainAppViewModel;
            ProjectViewModel = projectViewModel;
            EditableProjectViewModel = editableProjectViewModel;
        }
    }
}
