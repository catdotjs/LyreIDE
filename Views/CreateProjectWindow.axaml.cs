using Avalonia.Controls;
using Lyre.ViewModels;

namespace Lyre.Views;

public partial class CreateProjectWindow : Window{
    public CreateProjectWindow(){
        InitializeComponent();
    }

    protected override void OnClosing(WindowClosingEventArgs e){
        if(e.CloseReason==WindowCloseReason.WindowClosing){
            Window startWindow = new StartWindow(){DataContext= new StartWindowViewModel()};
            startWindow.Show();
        }
        
        base.OnClosing(e);
    }
}