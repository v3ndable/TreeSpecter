using System;
using System.IO;
using System.Collections.Generic;
using Spectre.Console;

class Program
{
    static List<string> ignorePatterns = new();

    static void Main(string[] args)
    {
        string path = Directory.GetCurrentDirectory();
        int maxDepth = int.MaxValue;
        bool showContent = false;
        bool copyMode = false;

        foreach (var arg in args)
        {
            if (arg.StartsWith("--depth="))
                int.TryParse(arg.Split('=')[1], out maxDepth);

            else if (arg == "--content")
                showContent = true;

            else if (arg == "--copy")
                copyMode = true;

            else if (arg.StartsWith("--ignore="))
                ignorePatterns.AddRange(arg.Split('=')[1].Split(',', StringSplitOptions.RemoveEmptyEntries));

            else if (arg.StartsWith("--ignore-file="))
                LoadIgnoreFile(arg.Split('=')[1]);

            else if (arg == "--help")
            {
                ShowHelp();
                return;
            }
            else
                path = arg;
        }

        if (!Directory.Exists(path))
        {
            AnsiConsole.Markup("[red]Pfad existiert nicht[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[bold green]TreeSpecter → {path}[/]\n");

        PrintDirectory(path, 0, maxDepth, showContent, copyMode);
    }

    static void LoadIgnoreFile(string filePath)
    {
        if (!File.Exists(filePath)) return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var trimmed = line.Trim();
            if (!string.IsNullOrWhiteSpace(trimmed) && !trimmed.StartsWith("#"))
                ignorePatterns.Add(trimmed);
        }
    }

    static bool ShouldIgnore(string name)
    {
        foreach (var pattern in ignorePatterns)
        {
            if (pattern.StartsWith("*"))
            {
                if (name.EndsWith(pattern.TrimStart('*')))
                    return true;
            }
            else if (string.Equals(name, pattern, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    static void ShowHelp()
    {
        AnsiConsole.MarkupLine("[bold yellow]TreeSpecter Help[/]\n");

        AnsiConsole.WriteLine("Usage:");
        AnsiConsole.WriteLine("  treespecter [path] [options]\n");

        AnsiConsole.WriteLine("Options:");
        AnsiConsole.WriteLine("  --depth=n         Max depth");
        AnsiConsole.WriteLine("  --content         Show file contents");
        AnsiConsole.WriteLine("  --copy            Output copy-friendly");
        AnsiConsole.WriteLine("  --ignore=list     Ignore files/folders");
        AnsiConsole.WriteLine("  --ignore-file     Load ignore file");
        AnsiConsole.WriteLine("  --help            Show help");
    }

    static void PrintDirectory(string path, int depth, int maxDepth, bool showContent, bool copyMode)
    {
        if (depth > maxDepth) return;

        string indent = new string(' ', depth * 2);
        string folderName = Path.GetFileName(path);

        if (ShouldIgnore(folderName)) return;

        if (!copyMode)
            AnsiConsole.MarkupLine($"{indent}[blue]{folderName}[/]");
        else
            Console.WriteLine($"{indent}{folderName}");

        try
        {
            foreach (var file in Directory.GetFiles(path))
            {
                string fileName = Path.GetFileName(file);
                if (ShouldIgnore(fileName)) continue;

                if (!copyMode)
                    AnsiConsole.MarkupLine($"{indent}  [white]- {fileName}[/]");
                else
                    Console.WriteLine($"{indent}  - {fileName}");

                if (showContent)
                {
                    try
                    {
                        string content = File.ReadAllText(file);

                        if (!copyMode)
                        {
                            AnsiConsole.MarkupLine($"{indent}    [grey]--- content ---[/]");
                            AnsiConsole.WriteLine($"{indent}    {content}");
                        }
                        else
                        {
                            Console.WriteLine($"{indent}    --- content ---");
                            Console.WriteLine(content);
                        }
                    }
                    catch
                    {
                        if (!copyMode)
                            AnsiConsole.MarkupLine($"{indent}    [red](cannot read)[/]");
                        else
                            Console.WriteLine($"{indent}    (cannot read)");
                    }
                }
            }

            foreach (var dir in Directory.GetDirectories(path))
            {
                string dirName = Path.GetFileName(dir);
                if (ShouldIgnore(dirName)) continue;

                PrintDirectory(dir, depth + 1, maxDepth, showContent, copyMode);
            }
        }
        catch
        {
            if (!copyMode)
                AnsiConsole.MarkupLine($"{indent}[red](no access)[/]");
            else
                Console.WriteLine($"{indent}(no access)");
        }
    }

}
