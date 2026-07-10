using Spectre.Console;

public class TreeSpecter
{
    public void Run(string[] args)
    {
        var options = Options.Parse(args);

        if (options.ShowHelp)
        {
            HelpPrinter.Show();
            return;
        }

        if (!Directory.Exists(options.Path))
        {
            AnsiConsole.MarkupLine("[red]Pfad existiert nicht[/]");
            return;
        }

        var ignoreService = new IgnoreService(options);

        AnsiConsole.MarkupLine(
            $"[bold green]TreeSpecter → {options.Path}[/]\n"
        );

        DirectoryPrinter.Print(
            options.Path,
            0,
            options,
            ignoreService
        );
    }
}