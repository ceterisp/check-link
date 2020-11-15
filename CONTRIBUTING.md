# Contributing to Check-Link

## Welcome!

Thank you for choosing to contribute to Check-Link!


## Issues

- Please create an issue with a breif explanation of the problem. 
- You are welcome to create any issue and work on it as you wish.

## Environment Setup
Check-Link requires you to install .NET in order to run the stand-alone application (without an IDE)

#### Linux distributions 
[Install .NET on Linux](https://docs.microsoft.com/en-us/dotnet/core/install/linux)

#### Windows 10 
[Install .NET on Windows](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net50)

## Technologies for developers

- It would be better to use an [Visual Studio IDE](https://visualstudio.microsoft.com/downloads/) to make your life a lot easier. 
- The NuGet package manager can install (restore) the packages required for this Check-Link to run.

#### CodeMaid
- This project uses a code styler and formatter known as CodeMaid (installed with NuGet packages).
- CodeMaid will help you to format the code according to PR accepted standards.
- Before you are ready to code, enable automatic cleanup on save in VS by `Ctrl`+`M,'`


#### dotnet-format
- Linux uses can take advantage of [dotnet-format](https://github.com/dotnet/format) to run and format the code using the CLI for PR acceptable standards.
- Find and run the bash script named `formatCode.sh` in scripts folder, to format the code. 

#### Resharper (Trial)
- This project also uses Resharper in VS, as well as in the CLI (Install [Resharper](https://www.jetbrains.com/help/resharper/ReSharper_Command_Line_Tools.html)).
- Find and run the bash script named `runLint.sh` in scripts folder. It will generate a txt file in xml format with description information about lints in the code.

## How to run the application in Visual Studio 2019/2017

- Click the green 'Code' dropdown button and select 'Download Zip'
- Extract the zip file and nagivate to the solution file CheckLinkCLI2.sln
- Open the solution using Visual Studio and build the application 
- Change the string variable `linkfile` and `htmlfile` to your .txt and .html file paths(absolute path) accordingly 
- Press `Ctrl` + `F5` and run the application


## Workflow in Git and GitHub

We use a number of tools and automated processes to help make it easier for
everyone to collaborate on Telescope. This includes things like auto-formatting
code, linting, and automated testing. We also use git and GitHub in particular
ways.

For more information on working with our tools and our workflows, see our [Git Workflow documentation](git-workflow.md).

## Reports

We have a number of automated reports and audits that can be run on the code.
These include things like checking accessibility and performance issues in our
frontend, and determining test coverage for our automated tests.

For more information on working with these automated reports, see our [Reports documentation](reports.md).

## Releases

When doing a release of Telescope, a number of steps must be done. To help our
maintainers do this properly, we have tools and information in our [Release documentation](release.md).
