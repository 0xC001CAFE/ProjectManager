using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels.Locator
{
    public interface IViewModelLocator
    {
        MainAppViewModel MainAppViewModel { get; }

        ProjectViewModel ProjectViewModel { get; }

        EditableProjectViewModel EditableProjectViewModel { get; }
    }
}
