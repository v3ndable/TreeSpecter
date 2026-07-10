using System;
using System.Collections.Generic;
using System.IO;

public class TreeOptions
{
    public string Path { get; set; } =
        Directory.GetCurrentDirectory();

    public int MaxDepth { get; set; } =
        int.MaxValue;

    public bool ShowContent { get; set; }

    public bool CopyMode { get; set; }

    public bool ShowHelp { get; set; }

    public List<string> IgnorePatterns { get; } = new();
}