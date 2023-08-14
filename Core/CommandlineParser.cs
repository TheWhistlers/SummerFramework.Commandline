using System;
using System.Collections.Generic;
using System.Reflection;
using SummerFramework.Commandline.Core.Attributes;

namespace SummerFramework.Commandline.Core;

public class CommandlineParser
{
    private List<string> commands;
    private List<Command> Commands = new List<Command>();

    public Type CommandClass { get; private set; }

    private CommandlineParser(List<string> commands, Type command_class)
    {
        this.commands = commands;
        this.CommandClass = command_class;

        construct_commands_from_class();
        generate_help();
        run_command();
    }

    // Rules: <project-name> <command_name> [args]*
    // [args]: -<arg_name>
    public static CommandlineParser Parse<T>(params string[] cmd)
    {
        return new CommandlineParser(new List<string>(cmd), typeof(T));
    }

    //private bool IsRequiredArg(string arg) => arg.StartsWith("@");
    //private bool IsOptionalArg(string arg) => arg.StartsWith("--");

    private void construct_commands_from_class()
    {
        foreach (var method in CommandClass.GetMethods())
        {
            if (!has_attribute<CommandNameAttribute>(method, out _))
                continue;

            var command_name = method.GetCustomAttribute<CommandNameAttribute>()!.Name;

            string command_alias;
            string command_desp = string.Empty;

            if (has_attribute<AliasOfCommandAttribute>(method, out var aca))
                command_alias = aca!.Alias;

            if (has_attribute<DescriptionOfCommandAttribute>(method, out var dca))
                command_desp = dca!.Description;

            var args = new List<CommandArgument>();
            var arg_names = new List<string>();
            var arg_desps = new Dictionary<string, string>();
            var arg_aliases = new Dictionary<string, string>();
            var arg_alias_mapping = new Dictionary<string, CommandArgument>();

            method.GetParameters().ToList().ForEach(p => arg_names.Add(p.Name!));

            if (method.GetCustomAttributes<AliasOfArgumentAttribute>() != null)
            {
                var aocs = method.GetCustomAttributes<AliasOfArgumentAttribute>();

                foreach (var aoc in aocs)
                    arg_aliases.Add(aoc.Argument, aoc.Alias);
            }

            if (method.GetCustomAttributes<DescriptionOfArgumentAttribute>() != null)
            {
                var doas = method.GetCustomAttributes<DescriptionOfArgumentAttribute>();
                
                foreach (var doa in doas)
                    arg_desps.Add(doa.Argument, doa.Description);
            }

            var temps = new List<string>();
            foreach (var p in method.GetParameters())
            {
                string a;

                if (arg_aliases.ContainsKey(p.Name!))
                    a = arg_aliases[p.Name!];
                else
                    a = p.Name!;

                temps.Add(a);
            }
            

            for (int i = 0; i < temps.Count; i++)
            {
                arg_desps.TryGetValue(arg_names[i], out var desp);
                args.Add(new CommandArgument(
                    arg_names[i],
                    desp ?? string.Empty,
                    method.GetParameters()[i].ParameterType
                    ));

                arg_alias_mapping.Add(temps[i], args[i]);
            }

            this.Commands.Add(new Command(
                command_name,
                command_desp,
                method,
                arg_alias_mapping));
        }
    }

    private void generate_help()
    {
        var action = delegate ()
        {
            Console.WriteLine("Name | Description");
            Console.WriteLine("---------------------");
            foreach (var cmd in Commands)
            {
                Console.WriteLine("Commands:");
                Console.WriteLine($"{cmd.Name} | {cmd.Description}");
                if (cmd.Arguments.Count >= 1)
                {
                    Console.WriteLine("Arguments:");
                    foreach (var arg in cmd.Arguments)
                    {
                        Console.WriteLine($"{arg.Name} | {arg.Description}");
                    }
                }
                Console.WriteLine("---------------------");
            }
        };

        var help_cmd = new Command("help", "List all the description of all the commands.", action.Method, new Dictionary<string, CommandArgument>());
        Commands.Add(help_cmd);
    }

    private void run_command()
    {
        var target_command = this.Commands.Find(c => c.Name.Equals(commands[0]));
        var target_method = target_command?.CommandMethod!;
        var args = new List<object?>();

        foreach (var arg in target_command?.Arguments!)
        {
            int index = (int)(target_command?.Arguments!.IndexOf(arg) + 1)!;

            if (index >= commands.Count)
                args.Add(target_method.GetParameters().ToList()
                    .Find(p => p.Name.Equals(arg.Name))?.DefaultValue);
            else
                args.Add(Convert.ChangeType(commands[index], arg.ArgumentType));

        }

        if (commands[0] == "help")
            target_method.Invoke(this, null);
        else
            target_method.Invoke(null, args.ToArray());
    }

    private static bool has_attribute<T>(MethodInfo target, out T? attr) where T : Attribute
    {
        attr = target.GetCustomAttribute<T>();
        return (target.GetCustomAttribute<T>() != null);
    }
}
