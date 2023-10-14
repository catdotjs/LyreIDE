using Lyre.Views;
using Avalonia.Controls;

namespace Lyre.ViewModels;
public class StartWindowVM : ViewModelBase {
    public StartWindowVM(Window window){
        baseWindow = window;
    }
    public void CreateNewProject(){
        Window createProject = new CreateProjectWindow(baseWindow);
        createProject.Show();
        // This is a design choice :^Pc
        baseWindow.Close();
    }
}
