using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI;

namespace Lyre.ViewModels;
public class CreateProjectVM : ViewModelBase {
    // Project Path
    private static Uri projectPath = new Uri("/");
    public Uri ProjectPath {
        get => projectPath; 
        set => this.RaiseAndSetIfChanged(ref projectPath,value);
    }
    
    // DotNet Versions
    private static List<string> versions = new();
    public static List<string> Versions { get => versions;}
    
    // Init
    public CreateProjectVM(IWindow iwindow){
        usedWindow = iwindow;
        AsyncFunctions().GetAwaiter().GetResult();
    }

    public async Task AsyncFunctions(){
        versions = await DotNetInfo.GetVersions();
    }

    /// <summary>
    ///  Used for selecting path 
    /// </summary>
    /// <returns> void/Task </returns>
    public async Task ChooseProjectPath(){
        ProjectPath = (await FileSystem.GetFolderPath(usedWindow.Current))[0];
    }
}