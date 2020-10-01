# Check-Link

Command-Line Interface (CLI) application to check if a web link is active or broken.

## How to run the application in Powershell/CMD

- Download the executable file [CheckLinkCLI2.exe](https://github.com/abuZayed15/check-link/releases/download/0.1/CheckLinkCLI2.exe) in any local folder of your choice
- Open Powershell or cmd.exe and navigate to the file path where executable is downloaded
- Simply type `.\CheckLinkCLI2.exe` + the absolute path to your .txt or .html file 
- If the passed file or file path is a directory, the application will read all the files in this directory
- The application will then parse through all the links in the file and return the http status codes to the cli

## How to run the application in Visual Studio 2019/2017

- Click the green 'Code' dropdown button and select 'Download Zip'
- Extract the zip file and nagivate to the solution file CheckLinkCLI2.sln
- Open the solution using Visual Studio and build the application 
- Change the string variable `linkfile` and `htmlfile` to your .txt and .html file paths(absolute path) accordingly 
- Press `Ctrl` + `F5` and run the application

## How to contribute to Check-Link

- COMING SOON!
