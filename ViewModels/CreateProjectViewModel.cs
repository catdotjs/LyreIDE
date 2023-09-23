using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

#pragma warning disable CS4014

namespace Lyre.ViewModels;
public class CreateProjectViewModel : ViewModelBase {
    private readonly static List<string> versions = new();
    public static List<string> Versions { get{
        GetDotNetVersions();
        return versions;
    }}
    /// <summary>
    /// Gets current SDK's and writes the versions to ComboBox
    /// </summary>
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
