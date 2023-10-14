using System;
using Lyre.Views;
using Avalonia.Controls;
using Lyre.Windows;

namespace Lyre.ViewModels;
public class StartWindowVM : ViewModelBase {
    public StartWindowVM(Window window){
        baseWindow = window;
    }
    public void CreateNewProject() => OpenUpWindow(new CreateProjectWindow(baseWindow));
    public void OpenProject() => OpenUpWindow(new OpenProjectWindow(baseWindow));
    public void PullGitProject() => OpenUpWindow(new GitPullWindow(baseWindow));
    public void ShowCredits() => OpenUpWindow(new CreditsWindow(baseWindow));

    private void OpenUpWindow(Window window){
        window.Show();
        // This is a design choice :P
        baseWindow.Close();
    }
}
