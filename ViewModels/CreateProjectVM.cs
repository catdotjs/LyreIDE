using System;
using System.Collections.Generic;
using System.Linq;
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
    
    // dotnet templates 
    private static Dictionary<string, string> templates = new();

    // used for ComboBox
    public static List<string> Templates { get => templates.Values.ToList();}
    // Init
    public CreateProjectVM(IWindow iwindow){
        usedWindow = iwindow;
    }
    
    public static async Task LoadAsync(){
        templates = await DotNetHandler.GetTemplates();
    }

    public async Task ChooseProjectPath(){
        ProjectPath = (await FileSystem.GetFolderPath(usedWindow.Current))[0];
    }
    
    public async Task CreateNewProject(){
        
    }
}