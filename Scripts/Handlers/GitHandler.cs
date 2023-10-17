using System;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Lyre.CLI.git;
using Serilog;

namespace Lyre.CLI;
/// <summary>
/// Handles git and git CLI
/// </summary>
static class GitHandler{
    private static Command gitWrap = Cli.Wrap("git");

    /// <summary>
    /// Used to check if git is installed
    /// </summary>
    /// <returns>Task/void</returns>
    public static async Task CheckGit(){
                Log.Information("Checking if git is installed");

        try{
            BufferedCommandResult gitInfo = await gitWrap.WithArguments("-v").ExecuteBufferedAsync();
            Log.Information("git is installed! "+gitInfo.StandardOutput.Trim());
        }catch(Exception e){
            Log.Fatal("MISSING GIT! ABORTING!",e);
            throw new FileNotFoundException("Missing git! Aborting!");
        }
    }

    /// <summary>
    /// Clones a given git repo
    /// </summary>
    /// <param name="repoLink">Hyperlink for git repo</param>
    /// <returns>bool(failed/success)</returns>
    public static async Task<bool> CloneRepo(Uri repoLink, Uri location, GitCloneFlags flags = GitCloneFlags.none, string additionalArguments=""){
        // Process flags
        bool insideFolder = flags.HasFlag(GitCloneFlags.InFolder);
        bool recursive    = flags.HasFlag(GitCloneFlags.Recursive);

        Log.Information($"Trying to clone a repo \"{repoLink}\" to \"{location}\" with flag {flags} and arguments \"{additionalArguments}\"");
        try {
            await gitWrap.WithArguments($"clone {repoLink} {location}{(insideFolder?"/.":"")} {(recursive?"--recurse-submodules":"")}").ExecuteAsync();
            Log.Information($"Success! Cloned {repoLink} to {location}!");

            return true;
        }catch(Exception e){
            Log.Error("Failed to clone repo, missing git?",e);
            throw new Exception("Failed to clone given repo!");
        }
    }
}