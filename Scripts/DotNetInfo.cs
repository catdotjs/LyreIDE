using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace Lyre;

static class DotNetInfo{
    /// <summary>
    /// Gets current SDK's and writes the versions to ComboBox
    /// </summary>
    /// <returns> Task<List<string>> </returns>
    public static async Task<List<string>> GetVersions(){
        // example output:(oldest to newest version)
        // 6.0.414 [/sdk/path/]
        // 7.0.400 [/sdk/path/]
        BufferedCommandResult resultBuffer = await Cli.Wrap("dotnet").WithArguments("--list-sdks").ExecuteBufferedAsync();
        string[] versionStringArray = resultBuffer.StandardOutput.TrimEnd().Split("\n");

        // Reverse list so newer SDK's are upper
        versionStringArray = versionStringArray.Reverse().ToArray();

        List<string> versions = versionStringArray.Select(x=>x[0]).Distinct().Select(x=>$".NET {x}.0").ToList();
        return versions;
    }
}