using System;

namespace SummerFramework.Commandline.Core.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class CommandNameAttribute : Attribute
{
    public string Name { get; set; }

    public CommandNameAttribute(string name)
    {
        Name = name;
    }
}
