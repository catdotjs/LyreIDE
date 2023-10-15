
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Serilog;

/// <summary>
/// OS interactions and data goes here
/// </summary>
public static class OSInteraction {
    /// <summary>
    /// Dictionary of which commands open any given file/link
    /// </summary>
    public static Dictionary<OSPlatform,string> OpenCommandsName = new(){
        {OSPlatform.Linux,"xdg-open"},
        {OSPlatform.FreeBSD,"xdg-open"},
        {OSPlatform.OSX,"open"},
        {OSPlatform.Windows,"start"}
    }; 
        
    /// <summary>
    /// Opens a website/file on users prefered application
    /// </summary>
    /// <param name="url">Valid URI</param>
    public static void Open(Uri url) => Open(url.OriginalString);
    /// <summary>
    /// Opens a website/file on users prefered application
    /// </summary>
    /// <param name="url">Valid Link or Path</param>
    public static void Open(string url){
        foreach(KeyValuePair<OSPlatform,string> pair in OpenCommandsName){
            if(RuntimeInformation.IsOSPlatform(pair.Key)){
                Process.Start(pair.Value,url);
                Log.Information($"Opened {url} using {pair.Value} system is {pair.Key}");
            }
        }
    }
}