using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Lyre.ViewModels;
using Lyre.Views;

namespace Lyre;

public partial class App : Application{
    public override void Initialize(){
        AvaloniaXamlLoader.Load(this);
    }
    public override void OnFrameworkInitializationCompleted(){
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop){
            desktop.MainWindow = new StartWindow();
        }
        base.OnFrameworkInitializationCompleted();
    }
}