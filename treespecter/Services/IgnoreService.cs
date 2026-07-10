using System;
using System.Collections.Generic;

public class IgnoreService
{
    private readonly List<string> patterns;


    public IgnoreService(TreeOptions options)
    {
        patterns = options.IgnorePatterns;
    }


    public bool ShouldIgnore(string name)
    {
        foreach (var pattern in patterns)
        {
            if (pattern.StartsWith("*"))
            {
                if (name.EndsWith(
                    pattern.TrimStart('*')))
                    return true;
            }
            else if (
                string.Equals(
                    name,
                    pattern,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}