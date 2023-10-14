using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using ReactiveUI;

using Lyre.Extends;
using Avalonia.Controls;


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
    [Required] 
    public string Template {
        get {return _Template;}
        set {
            if(string.IsNullOrEmpty(value)){
                throw new ArgumentNullException("You must choose a template!");
            }else{
                this.RaiseAndSetIfChanged(ref _Template,templates[value]);
            }
        }
    }

    // Project Name
    private string _Name = ""; 
    [Required] 
    public string Name {
        get { return _Name; } 
        set {
            if(string.IsNullOrEmpty(value)){
                throw new ArgumentNullException("You must give a name to your project!");
            }else{
                this.RaiseAndSetIfChanged(ref _Name,value.ReplaceMultiple(new char[]{' ','\\','\"','/'},'-'));
            }
        } // no no characters
    } 

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
    public CreateProjectVM(Window window) => baseWindow = window;
    public static async Task LoadAsync() => templates = await DotNetHandler.GetTemplates();
    public async Task ChooseProjectPath() => ProjectPath = (await FileSystem.GetFolderPath(baseWindow))[0];
    public async Task CreateNewProject(){
        bool nameGiven=_Name!="";
        bool templateChosen=_Template!="";
        if(nameGiven && templateChosen){
            ProjectGeneratationOptions file = _file?ProjectGeneratationOptions.tofile:0;
            ProjectGeneratationOptions sln = _sln?ProjectGeneratationOptions.sln:0;
            ProjectGeneratationOptions git = _git?ProjectGeneratationOptions.gitignore:0;

            string path = _Path.ToString().Remove(0,7);

            DotNetCreate package = new DotNetCreate(_Name,path,_Template,_Args,file|sln|git); //file://
            await DotNetHandler.CreateProject(package);
        }
    }
}