using System;
using Avalonia.Controls;

using Lyre.ViewModels;
using Lyre.Windows;

namespace Lyre.Views;
public partial class CreateProjectWindow : ReturningWindow{
    public CreateProjectWindow(Window returnWindow) : base(returnWindow){
        InitializeComponent();
        DataContext = new CreateProjectVM(this);
    }
}