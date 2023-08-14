using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SummerFramework.Commandline.Core;

public class Command
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CommandArgument> Arguments { get; set; }
    public MethodInfo CommandMethod { get; set; }

    // Alias: string <=> Argument: CommandArgument
    protected Dictionary<string, CommandArgument> args_alias_mapping = new();

    public Command(string name, string desp, MethodInfo cm, Dictionary<string, CommandArgument> args)
    {
        Name = name;
        Description = desp;
        Arguments = new List<CommandArgument>(args.Values);
        CommandMethod = cm;

        this.args_alias_mapping = args;
    }
}
