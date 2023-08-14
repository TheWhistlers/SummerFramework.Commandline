using System;
using System.Collections.Generic;

namespace SummerFramework.Commandline.Core.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class AliasOfCommandAttribute : Attribute
{
    public string Alias { get; set; }

    public AliasOfCommandAttribute(string alias)
    {
        Alias = alias;
    }
}
