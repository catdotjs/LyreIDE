/// <summary>
/// Used as a information package for DotNetHandler.CreateProject
/// </summary>
namespace Lyre.CLI.dotnet;
struct DotNetCreate{
    public string ProjectName;
    public string ProjectPath;
    public string ProjectType; // Template
    public string ExtraArguments;
    public ProjectGeneratationOptions ProjectGenOptions;

    public DotNetCreate(string projectName, string projectPath,string projectType,string extraArguments,ProjectGeneratationOptions generateProjectOptions){
        ProjectName = projectName;
        ProjectPath = projectPath;
        ProjectType = projectType;
        ProjectGenOptions = generateProjectOptions;
        ExtraArguments = extraArguments;
    }
}