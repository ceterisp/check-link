# Check-Link

![.NET Core](https://github.com/abuZayed15/check-link/workflows/.NET%20Core/badge.svg?branch=master)

Command-Line Interface (CLI) application to check if a web link is active or broken.

## How to run the application in Linux shell

- Linux distribution users can download the application from the [latest release](https://github.com/abuZayed15/check-link/releases/tag/v1.0.1).
- Once downloaded, simply use the application by running `./CheckLinkCLI2` followed the path to your file that stores the web links. For ease, simply move to the current directory where application is downloaded.
- If the passed file or file path is a directory, the application will read all the files in this directory.
- The application will then parse through all the links in the file and return the http status codes to the cli.

## How to run the application in Powershell/CMD

- Windows users can download the application from the [latest release](https://github.com/abuZayed15/check-link/releases/tag/v1.0.0) in any local folder of your choice.
- Open Powershell or cmd.exe and navigate to the directory where executable is downloaded.
- To run the application simply type `.\CheckLinkCLI2.exe` + the path to your .txt or .html file. 
- If the passed file or file path is a directory, the application will read all the files in this directory
- The application will then parse through all the links in the file and return the http status codes to the cli

## Features

### Version

- The follow command will return the current version of the application.

- Type '.\CheckLinkCLI2.exe' + -v or --version


### Filtered results using support flag `--all` `--good` `--bad`

- Adding flags such as `--good` will return all the links with status `200`. For example, `.\CheckLinkCLI2.exe` + `C:\Directory\fileName` +`--good`
- Adding flags like `--bad` will return all the links are not `OK` . For example, `.\CheckLinkCLI2.exe` + `C:\Directory\fileName` +`--bad`
- Adding the flag `--all` will simply return show all the links and their statuses. Its equilavent to not adding any flag at all. 

### Writing to a JSON file

- This feature will write all the links and its status code including helpful things like link statuses (e.g. good or bad) into a JSON file. 

- Type `.\CheckLinkCLI2.exe` + `C:\Directory\fileName` +`-j` or `\j` or `--json`

### Apply patterns to ignore specific urls

- This feature requires an **ignore file** with url patterns. It can only include comments (starts with `#`) and/or url patterns that starts with `http://` or `https://`. 
- Type `.\CheckLinkCLI2.exe` + `C:\Directory\ignorePatternFileName` + `C:\Directory\filename` + `-ignore` or `-i` or `\i`

### Reading JSON API

- This feature requires you to run the backend of [Telescope](https://github.com/Seneca-CDOT/telescope). It will check the first 10 posts and return whether the httpstatus to the console. 
- Type `.\CheckLinkCLI2.exe` + `telescope`


## How to contribute to Check-Link

- You are always welcome to contribute to Check-Link, see the [Contributing documentation](CONTRIBUTING.md) 
