using System;
using System.IO;
using Spectre.Console;

public static class DirectoryPrinter
{

    public static void Print(
        string path,
        int depth,
        TreeOptions options,
        IgnoreService ignore)
    {

        if (depth > options.MaxDepth)
            return;


        string indent =
            new string(' ', depth * 2);


        string folderName =
            new DirectoryInfo(path).Name;


        if (ignore.ShouldIgnore(folderName))
            return;


        Write(
            options,
            $"{indent}[blue]{folderName}[/]",
            $"{indent}{folderName}"
        );


        try
        {

            foreach (var file in Directory.GetFiles(path))
            {
                string fileName =
                    Path.GetFileName(file);


                if (ignore.ShouldIgnore(fileName))
                    continue;


                Write(
                    options,
                    $"{indent}  [white]- {fileName}[/]",
                    $"{indent}  - {fileName}"
                );


                if (options.ShowContent)
                {
                    try
                    {
                        string content =
                            File.ReadAllText(file);


                        Write(
                            options,
                            $"{indent}    [grey]--- content ---[/]",
                            $"{indent}    --- content ---"
                        );


                        if (options.CopyMode)
                        {
                            Console.WriteLine(content);
                        }
                        else
                        {
                            AnsiConsole.WriteLine($"{indent}    {content}");
                        }
                    }
                    catch
                    {
                        Write(
                            options,
                            $"{indent}    [red](cannot read)[/]",
                            $"{indent}    (cannot read)"
                        );
                    }
                }
            }


            foreach (var dir in Directory.GetDirectories(path))
            {
                Print(
                    dir,
                    depth + 1,
                    options,
                    ignore
                );
            }

        }
        catch
        {
            Write(
                options,
                $"{indent}[red](no access)[/]",
                $"{indent}(no access)"
            );
        }
    }



    private static void Write(
        TreeOptions options,
        string markup,
        string plain)
    {
        if (options.CopyMode)
            Console.WriteLine(plain);
        else
            AnsiConsole.MarkupLine(markup);
    }

}