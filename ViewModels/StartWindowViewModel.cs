using Lyre.Views;
using Avalonia.Controls;

namespace Lyre.ViewModels;
#pragma warning disable CA1822
public class StartWindowViewModel : ViewModelBase {
    public void CreateNewProject(){
        Window createProject = new CreateProjectWindow(){DataContext = new CreateProjectViewModel()};
        createProject.Show();

        // This is a design choice :^Pc
        StartWindow.CurrentWindow.Close();
    }
}
