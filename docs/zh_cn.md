![ICON](../sfcmd_icon.png)

# SummerFramework.Commandline的官方文档

## 快速开始

1. 下载此Nuget并导入至项目里。
2. 创建一个名为 `MyCommandClass`的类，然后向下面这样创建一些静态的方法 ↓

```c#

using SummerFramework.Commandline.Core;

public class MyCommandClass
{
    [CommandName("hello_world")]
    [AliasOfCommand("hi")]
    [DescriptionOfCommand("根据你的名字问好")]
    [DescriptionOfArgument("你的名字")]
    public static void HelloWorld(string name = "you")
    {
        Console.WriteLine($"Hello, {name}");
    }
}

```

`CommandName`特性类用于为此命令起名，然而有的时候我们会觉得这个名字太长。

那我们就可以使用 `AliasOfCommand`特性类来为我们的命令设置一个别名

该方法的参数会被映射成命令的参数，我们可以通过给定默认值的方法来使其变成可选参数。

我们也可以使用 `DescriptionOfCommand`和 `DescriptionOfArgument`特性类来为我们的命令及其参数设置描述介绍。

3. 然后我们在Main方法中只需要写一句话来使用 `CommandParser`类中的 `Parse<T>(string[] args)`即可启用命令解析功能了。

```c#

static void Main(string[] args) => CommandParser.Parse<MyCommandClass>(args);

```

4. 我们可以运行`.\Test.exe "Mike"`命令来测试我们刚刚创建的命令。

5. SummerFramework.Commandline为每一个命令类都内置了一个`help`命令来显示我们所设置的描述。