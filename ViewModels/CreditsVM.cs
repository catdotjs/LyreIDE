using Avalonia.Controls;

namespace Lyre.ViewModels;
public class CreditsVM : ViewModelBase {
    public CreditsVM(Window window){
        baseWindow = window;
    }
    public void ReturnToMenu(){
        baseWindow.Close();
    }
}
