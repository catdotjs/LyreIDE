using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using ReactiveUI;

#pragma warning disable CS4014

namespace Lyre.ViewModels;
public class CreateProjectVM : ViewModelBase {
    // Project Path
    private static Uri projectPath = new Uri("/");
    public Uri ProjectPath {
        get => projectPath; 
        set => this.RaiseAndSetIfChanged(ref projectPath,value);
    }
    
    // DotNet Versions
    private readonly static List<string> versions = new();
    public static List<string> Versions { get{
        GetDotNetVersions();
        return versions;
    }}
    
    // Init
    public CreateProjectVM(IWindow iwindow){
        usedWindow = iwindow;
    }

    /// <summary>
    ///  Used for selecting path 
    /// </summary>
    /// <returns> void/Task </returns>
    public async Task ChooseProjectPath(){
        ProjectPath = (await FileSystem.GetFolderPath(usedWindow.Current))[0];
    }
    
    /// <summary>
    /// Gets current SDK's and writes the versions to ComboBox
    /// </summary>
    /// <returns> void/Task </returns>
    private static async Task GetDotNetVersions(){
        // example output:(oldest to newest version)
        // 6.0.414 [/sdk/path/]
        // 7.0.400 [/sdk/path/]
        BufferedCommandResult resultBuffer = await Cli.Wrap("dotnet").WithArguments("--list-sdks").ExecuteBufferedAsync();
        string[] versionStringArray = resultBuffer.StandardOutput.TrimEnd().Split("\n");
        
        foreach(string version in versionStringArray.Reverse<string>()){
            // Style it for ComboBox
            string versionNumber =$".NET {version[0]}.0";

            if(!versions.Contains(versionNumber)){
                versions.Add(versionNumber);
            }
        }
    }
}