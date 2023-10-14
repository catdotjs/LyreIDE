using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lyre.ViewModels;
using Lyre.Windows;

namespace Lyre.Views;

public partial class CreditsWindow : ReturningWindow{
    
    public CreditsWindow(Window returnWindow) : base(returnWindow){
        InitializeComponent();
        DataContext = new GitPullVM(this);
    }
}