using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using CliWrap;
using CliWrap.Buffered;

using Serilog;

using Lyre.Extends;
using Lyre.CLI.dotnet;
using System.IO;

namespace Lyre.CLI;
/// <summary>
/// Anything related to .NET is handled here 
/// </summary>
static class DotNetHandler{
    private static Command dotnetWrap = Cli.Wrap("dotnet");

    /// <summary>
    /// Checks if user has dotnet installed! 
    /// </summary>
    /// <returns>Task/void</returns>
    public static async Task CheckDotNet(){
        Log.Information("Checking if dotnet is installed");

        try{
            BufferedCommandResult dotnetInfo = await dotnetWrap.WithArguments("--version").ExecuteBufferedAsync();
            Log.Information("dotnet is installed! dotnet version "+dotnetInfo.StandardOutput);
        }catch(Exception e){
            Log.Fatal("MISSING DOTNET! ABORTING!",e);
            throw new FileNotFoundException("Missing dotnet!");
        }
    }
    /// <summary>
    /// Gets project templates from dotnet CLI(this is a relatively slow function, please cache the results)
    /// </summary>
    /// <returns> Task<Dictionary<string,string>> | Key=long name/Value=short name</returns>
    /// <exception cref="Exception"> Thrown when dotnet cli fails to return anything </exception>
    public static async Task<Dictionary<string,string>> GetTemplates(){
        // dotnet new list --language C# --type project --columns language 
        // "--columns language" part shortens the whole thing so template name is readable
        try{
            Log.Information("Fetching available templates");
            BufferedCommandResult resultBuffer = await dotnetWrap.WithArguments("new list --language C# --type project --columns language").ExecuteBufferedAsync();
            Stack<string> rawStack = new(resultBuffer.StandardOutput.Trim().Split("\n").Reverse());

            // Removes excess stuff please ignore :)))))))
            rawStack.PopRange(4);
            
            Dictionary<string, string> templateDict = new();

            while(rawStack.Count>0){
                string[] test = rawStack.Pop().Split("  ").Where(x=>x!="").ToArray();
                templateDict.Add(test[0].Trim(),test[1].Trim());
            }

            Log.Information("Fetched available templates");
            return templateDict;
        }catch(Exception e){
            string failed = "Couldn't fetch Templates. Is dotnet installed correctly?";
            Log.Error(failed,e);
            throw new Exception(failed);
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
            Log.Information("Fetching available versions");
            BufferedCommandResult resultBuffer = await dotnetWrap.WithArguments("--list-sdks").ExecuteBufferedAsync();
            string[] versionStringArray = resultBuffer.StandardOutput.TrimEnd().Split("\n");

            // Reverse list so newer SDK's are upper
            versionStringArray = versionStringArray.Reverse().ToArray();

            // Gets dotnet version number
            List<string> versions = versionStringArray.Select(x=>x[0]).Distinct().Select(x=>$".NET {x}.0").ToList();
            Log.Information("Fetched available versions");
            return versions;
        }catch(Exception e){
            Log.Error(e,"Fetching SDKs");
            throw new Exception("Couldn't fetch SDKs. Is there any sdks installed?");
        }
    }

    /// <summary>
    /// Used to create a new dotnet project using the dotnet cli
    /// </summary>
    /// <param name="data">A struct with necesarry data</param>
    /// <returns>Task/Void</returns>
    public static async Task CreateProject(DotNetCreate data){
        try{
            string path = data.ProjectPath;
            ProjectGeneratationOptions options = data.ProjectGenOptions;

            if(!options.HasFlag(ProjectGeneratationOptions.tofile)){
                path+="/"+data.ProjectName;
            }

            string command = $"new {data.ProjectType} -o \"{path}\" -n \"{data.ProjectName}\" "+data.ExtraArguments;

            Log.Information("Running dotnet "+ command);
            await dotnetWrap.WithArguments(command).ExecuteAsync();

            if(options.HasFlag(ProjectGeneratationOptions.gitignore)){
                Log.Information("Creating a gitignore");
                await dotnetWrap.WithArguments("new gitignore -o "+path).ExecuteAsync();
            }

            if(options.HasFlag(ProjectGeneratationOptions.sln)){
                Log.Information("Creating a SLN file");
                await dotnetWrap.WithArguments("new sln -o "+path).ExecuteAsync();
                await dotnetWrap.WithArguments("sln add "+path).ExecuteAsync();
            }
            
            Log.Information("Successfully created a new project at " + data.ProjectPath);
        }catch(Exception e){
            string failed = "Couldn't create a new project. Is dotnet installed correctly? Were extra arguments wrong? Given extra args: "+data.ExtraArguments;
            Log.Error(failed,e);
            throw new Exception(failed);
        }
    }
}