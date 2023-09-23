using Avalonia;
using Avalonia.ReactiveUI;
using Lyre.Extras;
using Serilog;
using Serilog.Exceptions;
using System;
using System.IO;

namespace Lyre;

class Program {
    public static string? CurrentDirectory {get; private set;}
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(){        
        // Custom Logger
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.File($"Logs/Log-.log",rollingInterval: RollingInterval.Day)
            .CreateLogger();

        CurrentDirectory = Directory.GetCurrentDirectory();
        Log.Information($"App started at {CurrentDirectory}");

        // Stuff goes here
        SplashText.Load();

        // Don't touch here please
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }
}
