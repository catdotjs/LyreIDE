using System;

namespace Lyre.CLI.dotnet;
[Flags]
public enum ProjectGeneratationOptions{
    none = 0b0,
    sln = 0b1,
    gitignore = 0b10,
    tofile = 0b100,
}