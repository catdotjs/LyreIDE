using System;

namespace Lyre.CLI.git;
[Flags]
public enum GitCloneFlags{
    none = 0b0,
    InFolder = 0b1,
    Recursive = 0b10,
}
