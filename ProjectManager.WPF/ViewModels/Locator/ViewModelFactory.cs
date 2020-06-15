using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels.Locator
{
    public delegate T ViewModelFactory<T>() where T : ViewModelBase;
}
