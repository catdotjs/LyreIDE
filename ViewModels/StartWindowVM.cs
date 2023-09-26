using Lyre.Views;
using Avalonia.Controls;

namespace Lyre.ViewModels;
public class StartWindowVM : ViewModelBase {
    public StartWindowVM(IWindow iwindow){
        usedWindow = iwindow;
    }
    public void CreateNewProject(){
        Window createProject = new CreateProjectWindow();
        createProject.Show();
        // This is a design choice :^Pc
        usedWindow.Current.Close();
    }
}
