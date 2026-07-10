using Spectre.Console;


public static class HelpPrinter
{

    public static void Show()
    {
        AnsiConsole.MarkupLine(
            "[bold yellow]TreeSpecter Help[/]\n"
        );


        Console.WriteLine("Usage:");
        Console.WriteLine(
            "  treespecter [path] [options]\n"
        );


        Console.WriteLine("Options:");
        Console.WriteLine(
            "  --depth=n          Max depth"
        );

        Console.WriteLine(
            "  --content          Show file contents"
        );

        Console.WriteLine(
            "  --copy             Output copy-friendly"
        );

        Console.WriteLine(
            "  --ignore=list      Ignore files/folders"
        );

        Console.WriteLine(
            "  --ignore-file=file Load ignore file"
        );

        Console.WriteLine(
            "  --help             Show help"
        );
    }
}