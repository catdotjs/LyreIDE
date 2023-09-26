using Avalonia.Controls;
using Lyre.ViewModels;

namespace Lyre.Views;
public partial class CreateProjectWindow : Window, IWindow{
    Window IWindow.Current => this;
    public CreateProjectWindow(){
        InitializeComponent();
        DataContext = new CreateProjectVM(this);
    }

    protected override void OnClosing(WindowClosingEventArgs e){
        if(e.CloseReason==WindowCloseReason.WindowClosing){
            Window startWindow = new StartWindow();
            startWindow.Show();
        }
        base.OnClosing(e);
    }
}