using System;
using System.IO;

public static class Options
{
    public static TreeOptions Parse(string[] args)
    {
        var options = new TreeOptions();

        foreach (var arg in args)
        {
            if (arg.StartsWith("--depth="))
            {
                int.TryParse(
                    arg.Split('=')[1],
                    out int depth
                );

                options.MaxDepth = depth;
            }

            else if (arg == "--content")
            {
                options.ShowContent = true;
            }

            else if (arg == "--copy")
            {
                options.CopyMode = true;
            }

            else if (arg.StartsWith("--ignore="))
            {
                options.IgnorePatterns.AddRange(
                    arg.Split('=')[1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                );
            }

            else if (arg.StartsWith("--ignore-file="))
            {
                LoadIgnoreFile(
                    options,
                    arg.Split('=')[1]
                );
            }

            else if (arg == "--help")
            {
                options.ShowHelp = true;
            }

            else if (!arg.StartsWith("--"))
            {
                options.Path = arg;
            }

            else
            {
                Console.WriteLine($"Unknown option: {arg}");
            }
        }

        return options;
    }


    private static void LoadIgnoreFile(
        TreeOptions options,
        string filePath)
    {
        if (!File.Exists(filePath))
            return;


        foreach (var line in File.ReadAllLines(filePath))
        {
            var text = line.Trim();

            if (
                !string.IsNullOrWhiteSpace(text)
                &&
                !text.StartsWith("#")
            )
            {
                options.IgnorePatterns.Add(text);
            }
        }
    }
}