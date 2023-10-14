using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lyre.ViewModels;
using Lyre.Windows;

namespace Lyre.Views;

public partial class GitPullWindow : ReturningWindow{
    
    public GitPullWindow(Window returnWindow) : base(returnWindow){
        InitializeComponent();
        DataContext = new GitPullVM(this);
    }
}