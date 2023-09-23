using Avalonia.Controls;
using Lyre.Extras;

namespace Lyre.Views;
#pragma warning disable CA2211,CS8618
public partial class StartWindow : Window {
    public static Window CurrentWindow;
    public StartWindow(){
        InitializeComponent();
        // Linux 
        MaxHeight = MinHeight = 450;
        MaxWidth = MinWidth = 800;
        CurrentWindow = this;
        
        Title = $"Lyre IDE - {SplashText.GetRandom()}";
    }
}