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
