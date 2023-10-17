using Avalonia.Controls;

using Lyre.ViewModels;
using Lyre.Windows;

namespace Lyre.Views;

public partial class GitCloneWindow : ReturningWindow{
    
    public GitCloneWindow(Window returnWindow) : base(returnWindow){
        InitializeComponent();
        DataContext = new GitCloneVM(this);
    }
}