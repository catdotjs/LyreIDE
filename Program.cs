using Avalonia;
using Avalonia.ReactiveUI;
using Lyre.CLI;
using Lyre.Extras;
using Lyre.ViewModels;
using Serilog;
using Serilog.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Lyre;

class Program {
    public static string? CurrentDirectory {get; private set;}

    public static void OnStart(){
        // Init stuff goes here
        
        // Logging
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.File($"Logs/Log-.log",rollingInterval: RollingInterval.Day)
            .CreateLogger();

        CurrentDirectory = Directory.GetCurrentDirectory();
        Log.Information($"App started at {CurrentDirectory}");

        SplashText.Load();
    }
    public static async Task OnStartAsync(){
        // Any async start code goes here
        
        // Check important programs
        await DotNetHandler.CheckDotNet();
        await GitHandler.CheckGit();

        // Loading
        await CreateProjectVM.LoadAsync(); // Loads project templates
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static async Task Main(string[] args){
        // Start functions
        OnStart();
        await OnStartAsync();

        // Avalonia
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(){        
        // Don't touch here please
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }
}
