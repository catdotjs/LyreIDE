using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Serilog;

// Screw these
#pragma warning disable CS8600, CS8601, CS8602

namespace Lyre.Extras;
/// <summary>
/// Small class that is used for titlebar
/// </summary>
public static class SplashText {
    private static string[]? TextList;

    public static void Load(){
        try{
            // Read them from json
            JObject file = JObject.Parse(File.ReadAllText(Program.CurrentDirectory+"/Assets/SplashText.json"));
            JArray splashTextList = (JArray)file["SplashText"];
            TextList = splashTextList.ToObject<string[]>();

            Log.Information("Loaded SplashText.json!");
        }catch(Exception e){
            TextList = new string[]{"We couldn't find SplashText.json :("};
            Log.Error(e,"Loading SplashText.json");
        }
    }

    public static string GetRandom(){
        int ind = Random.Shared.Next(0,TextList.Length);
        return TextList[ind];
    }
}