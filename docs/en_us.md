![ICON](../sfcmd_icon.png)

# SummerFramework.Commandline's Documents

## Quick Start

1. Download Nuget Package and import it
2. Create a class named `MyCommandClass` and create some static methods like this ↓

```c#

using SummerFramework.Commandline.Core;

public class MyCommandClass
{
    [CommandName("hello_world")]
    [AliasOfCommand("hi")]
    [DescriptionOfCommand("To say Hello with your name")]
    [DescriptionOfArgument("Your Name")]
    public static void HelloWorld(string name = "you")
    {
        Console.WriteLine($"Hello, {name}");
    }
}

```

`CommandName` attribute is to name the command, and sometimes we may think the name is too long.

Then we can use `AliasOfCommand` attribute to set alias for the command.

The parameters of the method will map to the arguments of the command, we can give a default value to make it optional.

We can also use `DescriptionOfCommand` and `DescriptionOfArgument` to set description for the command and its arguments.

3. And then just write a single statement to use method `Parse<T>(string[] args)` in class `CommandParser` to enable command parse functions.

```c#

static void Main(string[] args) => CommandParser.Parse<MyCommandClass>(args);

```

4. Then we can run `.\Test.exe "Mike"` to run the command we have created.

5. SummerFramework.Commandline has built in a `help` command to display the description you set.s