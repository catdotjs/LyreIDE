using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Serilog;

namespace Lyre;
/// <summary>
/// Anything related to .NET is handled here 
/// </summary>
static class DotNetHandler{
    private static Command dotnetWrap = Cli.Wrap("dotnet");
    /// <summary>
    /// Gets project templates from dotnet CLI(this is a relatively slow function, please cache the results)
    /// </summary>
    /// <returns> Task<Dictionary<string,string>> </returns>
    /// <exception cref="Exception"> Thrown when dotnet cli fails to return anything </exception>
    public static async Task<Dictionary<string,string>> GetTemplates(){
        // dotnet new list --language C# --type project --columns language 
        // "--columns language" part shortens the whole thing so template name is readable
        try{
            BufferedCommandResult resultBuffer = await dotnetWrap.WithArguments("new list --language C# --type project --columns language").ExecuteBufferedAsync();
            Stack<string> rawStack = new(resultBuffer.StandardOutput.Trim().Split("\n").Reverse());

            // Removes excess stuff please ignore :)))))))
            rawStack.PopRange(4);
            
            Dictionary<string, string> templateDict = new();

            while(rawStack.Count>0){
                string[] test = rawStack.Pop().Split("  ").Where(x=>x!="").ToArray();
                templateDict.Add(test[1].Trim(),test[0].Trim());
            }

            return templateDict;
        }catch(Exception e){
            Log.Error(e,"Fetching Templates");
            throw new Exception("Couldn't fetch Templates. Is dotnet installed?");
        }
    }

    /// <summary>
    /// (UNUSED) Gets current SDK's from dotnet CLI(this is a relatively slow function, please cache the results)
    /// </summary>
    /// <returns> Task<List<string>> </returns>
    /// /// <exception cref="Exception"> Thrown when dotnet cli fails to return anything </exception>
    public static async Task<List<string>> GetVersions(){
        // example output:(oldest to newest version)
        // 6.0.414 [/sdk/path/]
        // 7.0.400 [/sdk/path/]
        try{
            BufferedCommandResult resultBuffer = await dotnetWrap.WithArguments("--list-sdks").ExecuteBufferedAsync();
            string[] versionStringArray = resultBuffer.StandardOutput.TrimEnd().Split("\n");

            // Reverse list so newer SDK's are upper
            versionStringArray = versionStringArray.Reverse().ToArray();

            // Gets dotnet version number
            List<string> versions = versionStringArray.Select(x=>x[0]).Distinct().Select(x=>$".NET {x}.0").ToList();
            return versions;
        }catch(Exception e){
            Log.Error(e,"Fetching SDKs");
            throw new Exception("Couldn't fetch SDKs. Is dotnet installed?");
        }
    }

    /// <summary>
    /// Used to create a new dotnet project using the dotnet cli
    /// </summary>
    /// <param name="data">A struct with necesarry data</param>
    /// <returns>Task/Void</returns>
    public static async Task CreateProject(DotNetCreate data){
        string path = data.ProjectPath;
        GenerateProjectOptions options = data.ProjectGenOptions;

        if(!options.HasFlag(GenerateProjectOptions.tofile)){
            path+="/"+data.ProjectName;
        }

        await dotnetWrap.WithArguments($"new {data.ProjectType} -o {path} -n {data.ProjectName} "+data.ExtraArguments).ExecuteAsync();

        if(options.HasFlag(GenerateProjectOptions.gitignore)){
            await dotnetWrap.WithArguments("new gitignore").ExecuteAsync();
        }

        if(options.HasFlag(GenerateProjectOptions.sln)){
            await dotnetWrap.WithArguments("new sln").ExecuteAsync();
            await dotnetWrap.WithArguments("sln add .").ExecuteAsync();
        }
    }
}