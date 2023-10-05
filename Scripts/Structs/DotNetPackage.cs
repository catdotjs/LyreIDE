using System;

/// <summary>
/// Used as a information package for DotNetHandler.CreateProject
/// </summary>
struct DotNetCreate{
    public string ProjectName;
    public string ProjectPath;
    public string ProjectType; // Template
    public string ExtraArguments;
    public GenerateProjectOptions ProjectGenOptions;

    public DotNetCreate(string projectName, string projectPath,string projectType,string extraArguments,GenerateProjectOptions generateProjectOptions){
        ProjectName = projectName;
        ProjectPath = projectPath;
        ProjectType = projectType;
        ProjectGenOptions = generateProjectOptions;
        ExtraArguments = extraArguments;
    }
}

[Flags]
public enum GenerateProjectOptions{
    sln = 0b1,
    gitignore = 0b10,
    tofile = 0b100,
}