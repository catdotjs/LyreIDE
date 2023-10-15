using System;
using Avalonia.Controls;
using Lyre.Views;

namespace Lyre.Windows;

/// <summary>
/// Returning windows have to return to window that generated them which is closed.
/// </summary>
#pragma warning disable CS8600, CS8602
public abstract class ReturningWindow : Window{
    // Window to return
    public Window WindowToGoOnUserClose;

    public ReturningWindow(Window returnWindow) : base(){
        WindowToGoOnUserClose = returnWindow;
    }
    /// <summary>
    /// Due to how avalonia works, We need to make a new instance
    /// This is quite a hack but works out
    /// </summary>
    protected override void OnClosing(WindowClosingEventArgs e){
        if(e.CloseReason==WindowCloseReason.WindowClosing && WindowToGoOnUserClose!=null){
            Window returnWindow = (Window)Activator.CreateInstance(WindowToGoOnUserClose.GetType()) ?? new StartWindow();
            returnWindow.Show();
        }
        base.OnClosing(e);
    }
}