namespace ProjectManager.WPF.ViewModels.Locator
{
    public delegate T ViewModelFactory<T>() where T : ViewModelBase;
}
