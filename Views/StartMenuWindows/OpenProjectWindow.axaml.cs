using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lyre.ViewModels;
using Lyre.Windows;

namespace Lyre.Views;

public partial class OpenProjectWindow : ReturningWindow{
    
    public OpenProjectWindow(Window returnWindow) : base(returnWindow){
        InitializeComponent();
        DataContext = new OpenProjectVM(this);
    }
}