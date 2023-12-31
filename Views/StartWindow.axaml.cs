using Avalonia.Controls;
using Lyre.Extras;
using Lyre.ViewModels;

namespace Lyre.Views;

public partial class StartWindow : Window {
    public StartWindow(){
        InitializeComponent();
        DataContext = new StartWindowVM(this);
        Title = $"Lyre IDE - {SplashText.GetRandom()}";
    }
}