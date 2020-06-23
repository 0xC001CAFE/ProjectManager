namespace ProjectManager.WPF.ViewModels.Locator
{
    public class ViewModelLocator : IViewModelLocator
    {
        public ViewModelFactory<MainAppViewModel> MainAppViewModel { get; }

        public ViewModelFactory<ProjectViewModel> ProjectViewModel { get; }

        public ViewModelFactory<EditableProjectViewModel> EditableProjectViewModel { get; }

        public ViewModelFactory<EditableTaskViewModel> EditableTaskViewModel { get; }

        public ViewModelLocator(ViewModelFactory<MainAppViewModel> mainAppViewModel,
                                ViewModelFactory<ProjectViewModel> projectViewModel,
                                ViewModelFactory<EditableProjectViewModel> editableProjectViewModel,
                                ViewModelFactory<EditableTaskViewModel> editableTaskViewModel)
        {
            MainAppViewModel = mainAppViewModel;
            ProjectViewModel = projectViewModel;
            EditableProjectViewModel = editableProjectViewModel;
            EditableTaskViewModel = editableTaskViewModel;
        }
    }
}
