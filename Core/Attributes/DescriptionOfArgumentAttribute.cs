using System;

namespace SummerFramework.Commandline.Core.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class DescriptionOfArgumentAttribute : Attribute
{
    public string Argument { get; set; }
    public string Description { get; set; }

    public DescriptionOfArgumentAttribute(string arg, string desp)
    {
        Argument = arg;
        Description = desp;
    }
}
