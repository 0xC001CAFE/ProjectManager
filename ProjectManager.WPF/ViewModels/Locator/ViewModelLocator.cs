using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels.Locator
{
    public class ViewModelLocator : IViewModelLocator
    {
        public MainAppViewModel MainAppViewModel { get; private set; }

        public ProjectViewModel ProjectViewModel { get; private set; }

        public EditableProjectViewModel EditableProjectViewModel { get; private set; }

        public ViewModelLocator(MainAppViewModel mainAppViewModel,
                                ProjectViewModel projectViewModel,
                                EditableProjectViewModel editableProjectViewModel)
        {
            MainAppViewModel = mainAppViewModel;
            ProjectViewModel = projectViewModel;
            EditableProjectViewModel = editableProjectViewModel;
        }
    }
}
