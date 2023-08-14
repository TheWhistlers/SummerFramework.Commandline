using System;
using System.Collections.Generic;
using System.Linq;

namespace SummerFramework.Commandline.Core;

public class CommandArgument
{
    public string Name { get; set; }
    public Type ArgumentType { get; set; }
    public string Description { get; set; }

    public CommandArgument(string name, string desp, Type arg_type)
    {
        Name = name;
        Description = desp;
        ArgumentType = arg_type;
    }
}
