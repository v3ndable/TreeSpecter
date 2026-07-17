<a id="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<div align="center">

# TreeSpecter
TreeSpecter is a command-line tool for displaying directory structures in a tree format.  
It supports recursive traversal and optional file content output.

</div>

## Requirements

- .NET SDK 8.0 or later

Check installation:

```bash
dotnet --version
```

## Features

* Recursive directory traversal with tree output
* Configurable maximum depth
* Optional file content display
* Ignore files and folders
* Plain text output mode
* Colored terminal output (Spectre.Console)
* Basic handling of access/permission errors

## Usage

### Run with .NET

```bash
dotnet run -- [path] [options]
```

### After installation / publish

```bash
treespecter [path] [options]
```

## Options

| Option             | Description                             |
| ------------------ | --------------------------------------- |
| --depth=n          | Maximum recursion depth                 |
| --content          | Show file contents                      |
| --copy             | Plain text output (no colors)           |
| --ignore=list      | Ignore files/folders                    |
| --ignore-file=path | Load ignore patterns from file          |
| --help             | Show help message                       |

### Save output to a file

For clean output including file contents, you can combine options and redirect the output:

```bash
treespecter --content --copy > output.txt
```

This produces a plain text file without colors or formatting.

### Ignore Examples

#### Inline ignore

```bash
treespecter --ignore=node_modules,.git,bin
```

#### Using a file

```bash
treespecter --ignore-file=.ignore
```

Supports simple patterns like:

```bash
node_modules
bin
obj
.git
*.log
```

## Installation

### Option 1: Build from source

```bash
git clone https://github.com/v3ndable/TreeSpecter.git
cd TreeSpecter
dotnet build
```

Run directly:

```bash
dotnet run -- [path] [options]
```

### Option 2: Publish executable

#### Windows

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

#### Linux

```bash
dotnet publish -c Release -r linux-x64 --self-contained true
```

The executable will be located in:

```
bin/Release/net8.0/<runtime>/publish/
```

## Global Installation (Recommended)

To run `treespecter` from anywhere:

### Windows

1. Publish the project

2. Copy the publish folder to a location of your choice, for example:

   ```
   C:\Tools\TreeSpecter\
   ```

3. Add it to PATH:
   * Start Menu → "Environment Variables"
   * Open "Edit system environment variables"
   * Environment Variables → Path → Edit → Add:
     ```
     C:\Tools\TreeSpecter\
     ```

4. Restart terminal

### Linux

```bash
sudo mv bin/Release/net8.0/linux-x64/publish/treespecter /usr/local/bin/
sudo chmod +x /usr/local/bin/treespecter
```

## Notes

* Large files are printed without filtering when using `--content`
* Access-restricted folders are skipped with a warning
* Ignore rules are simple pattern-based (not full gitignore spec)

## License
MIT — see [LICENSE](LICENSE) for details.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

[contributors-shield]: https://img.shields.io/github/contributors/v3ndable/TreeSpecter.svg?style=for-the-badge
[contributors-url]: https://github.com/v3ndable/TreeSpecter/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/v3ndable/TreeSpecter.svg?style=for-the-badge
[forks-url]: https://github.com/v3ndable/TreeSpecter/network/members
[stars-shield]: https://img.shields.io/github/stars/v3ndable/TreeSpecter.svg?style=for-the-badge
[stars-url]: https://github.com/v3ndable/TreeSpecter/stargazers
[issues-shield]: https://img.shields.io/github/issues/v3ndable/TreeSpecter.svg?style=for-the-badge
[issues-url]: https://github.com/v3ndable/TreeSpecter/issues
[license-shield]: https://img.shields.io/github/license/v3ndable/TreeSpecter.svg?style=for-the-badge
[license-url]: https://github.com/v3ndable/TreeSpecter/blob/master/LICENSE
