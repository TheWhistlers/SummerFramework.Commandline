using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerFramework.Commandline.Core.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AliasOfArgumentAttribute : Attribute
{
    public string Argument { get; set; }
    public string Alias { get; set; }

    public AliasOfArgumentAttribute(string arg, string alias)
    {
        Argument = arg;
        Alias = alias;
    }
}
