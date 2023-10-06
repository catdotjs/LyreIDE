using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ReactiveUI;


namespace Lyre.ViewModels;
public class CreateProjectVM : ViewModelBase {
    /// UI ELEMENTS   
    // dotnet Templates 
    private static Dictionary<string, string> templates = new();
    public static List<string> Templates { get => templates.Keys.ToList(); }
    
    /// Project Info
    // Project Path
    private static Uri _Path = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
    public Uri ProjectPath {get => _Path; set => this.RaiseAndSetIfChanged(ref _Path,value);}
    
    // Project Template - get here is unused
    private string _Template = ""; 
    public string ChosenTemplate {get => "";set => _Template=templates[value];}

    // Project Name
    private string _Name = ""; 
    public string Name {get=>_Name; set=>_Name=value.Replace(" ","-").Replace("\\","").Replace("\"","");} // no no characters

    // Additional Arguments
    private string _Args = ""; 
    public string Args {get=>_Args; set=>_Args=value;}

    // Additional Options
    private bool _file = false;
    private bool _sln = false;
    private bool _git = false;
    public bool InFile {get => _file; set => _file = value;}
    public bool CreateSLN {get => _sln; set => _sln = value;}
    public bool CreateGit {get => _git; set => _git = value;}

    // Init
    public CreateProjectVM(IWindow iwindow) => usedWindow = iwindow;
    public static async Task LoadAsync() => templates = await DotNetHandler.GetTemplates();
    public async Task ChooseProjectPath() => ProjectPath = (await FileSystem.GetFolderPath(usedWindow.Current))[0];
    public async Task CreateNewProject(){
        ProjectGeneratationOptions file = _file?ProjectGeneratationOptions.tofile:0;
        ProjectGeneratationOptions sln = _sln?ProjectGeneratationOptions.sln:0;
        ProjectGeneratationOptions git = _git?ProjectGeneratationOptions.gitignore:0;

        string path = _Path.ToString().Remove(0,7);

        DotNetCreate package = new DotNetCreate(_Name,path,_Template,_Args,file|sln|git); //file://
        await DotNetHandler.CreateProject(package);
    }
}