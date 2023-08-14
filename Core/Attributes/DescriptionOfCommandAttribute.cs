using System;

namespace SummerFramework.Commandline.Core.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class DescriptionOfCommandAttribute : Attribute
{
    public string Description { get; set; }

    public DescriptionOfCommandAttribute(string desp)
    {
        Description = desp;
    }
}
