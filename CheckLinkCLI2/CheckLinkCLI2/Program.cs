using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace CheckLinkCLI2
{
    public class Program
    {
        #region Debug or Release
        public static bool IsDebug()
        {
#if (DEBUG)
            return true;
#else
            return false;
#endif
        }

        #endregion

        #region variables to test
        private static readonly string linkFile = @"absolutePathToTxtFile.txt";
        private static readonly string htmlFile = @"D:\Documents\Seneca_College\OSD600\check-link2\check-link\CheckLinkCLI2\CheckLinkCLI2\index2.html";
        #endregion

        public static readonly List<string> version = new List<string>() { "v", "-v", "version", "--version" };
        public static readonly List<string> json = new List<string>() { "-j", @"\j", "--json" };
        public static readonly List<string> ignore = new List<string>() { "--ignore", "-i", @"\i" };
        public static readonly List<string> api = new List<string>() { "telescope" };
        public static Dictionary<string, List<string>> CommandLineOptions = new Dictionary<string, List<string>>()
        {
            {"api",api },
            {"version",version },
        };

        public static void Main(string[] args)
        {
            FileReader FileReader = new FileReader();
            WebLinkChecker LinkChecker = new WebLinkChecker();
            JsonApi jsonApi = new JsonApi();
            #region Dev env
            if (IsDebug())
            {

                foreach (var input in args)
                {
                    if (!File.Exists(input) && input.Contains(':') && !(input.EndsWith(".txt") || input.EndsWith(".html")) && input.StartsWith("http"))
                    {
                        LinkChecker.GetAllEndPointWithUri(input);
                        Console.WriteLine("\n");
                    }

                    else if (File.Exists(input))
                    {
                        if (!json.Contains(args.Last<string>()))
                        {
                            foreach (var link in FileReader.ExtractLinks(htmlFile))
                            {
                                string flag = LinkChecker.SetSupportFlag(args.Last<string>()) == null ? "--all" : LinkChecker.SetSupportFlag(args.Last<string>());
                                LinkChecker.GetAllEndPointWithUri(link, flag);
                            }
                            break;
                        }
                        else
                        {
                            FileReader.WriteToJSON(input);
                            Console.WriteLine($"Results of the links in {input} have been added to CheckLinkCLI2.json located in the current directory");
                            break;
                        }
                    }

                    else if (api.Contains(args.First<string>()))
                    {
                        jsonApi.ParseLinksFromJson();
                    }

                    else
                        Console.WriteLine($"File {input} does not exist");
                }
            }
            #endregion

            else
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide file name with links as an argument...");
                    Console.WriteLine("For example: CheckLinksCLI2 file_name.txt");

                }

                else if (api.Contains(args.First<string>()))
                {
                    Console.WriteLine($"===|Checking posts from : Telescope|===");
                    jsonApi.ParseLinksFromJson();
                }

                #region Command line options
                else if (version.Contains(args[0]))
                {
                    Console.WriteLine("Application Name: CheckLinkCLI2 \n" +
                        "Release: 0.1");
                }
                #endregion

                else if (ignore.Contains(args.Last<string>()))
                {
                    if (args.Length == 3)
                    {
                        var ignorePatternFile = args[0];
                        var sourceFile = args[1];
                        var patterns = FileReader.ReadIgnorePatterns(ignorePatternFile);
                        var links = FileReader.ExtractLinks(sourceFile);
                        var allowedLinks = links.Where(l => !patterns.Any(p => l.StartsWith(p)));
                        Console.Write("===|Reading file : ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{sourceFile}|===\n");
                        Console.ResetColor();
                        Console.Write("===|Ignore pattern file : ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{ignorePatternFile}|===\n");
                        Console.ResetColor();
                        foreach (var link in allowedLinks)
                        {
                            LinkChecker.GetAllEndPointWithUri(link);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your inputs for ignore feature are wrong. Please follow the format and try again.");
                        Console.WriteLine("For example: CheckLinksCLI2 ignore_pattern.txt file_name.txt --ignore");
                    }
                }

                else
                {
                    foreach (var file in args)
                    {
                        if (File.Exists(file) && !(file.EndsWith(".txt") || file.EndsWith(".html")))
                        {
                            Console.WriteLine($"Application is unable to parse {file} at this moment\n" +
                                "Please lookout for this feature in a future release\n" +
                                "Thank you for using CheckLinkCLI2!");
                        }

                        else if (!File.Exists(file) && file.Contains(':') && !(file.EndsWith(".txt") || file.EndsWith(".html")) && file.StartsWith("http"))
                        {
                            LinkChecker.GetAllEndPointWithUri(file);
                            Console.WriteLine("\n");
                        }

                        if (json.Contains(args.Last<string>()))
                        {
                            foreach (var link in FileReader.ExtractLinks(file))
                            {
                                FileReader.WriteToJSON(file);
                                Console.WriteLine($"\n\nResults of the links in {file} have been added to CheckLinkCLI2.json located in the current directory\n");
                                break;
                            }
                            break;
                        }

                        else
                        {
                            if (File.Exists(file) || Directory.Exists(file))
                            {
                                var links = FileReader.ExtractLinks(file);
                                WebLinkChecker wlc = new WebLinkChecker();

                                if (Directory.Exists(file))
                                {
                                    Console.Write("===|Reading all files in directory : ");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"{file}|===\n");
                                    Console.ResetColor();
                                    foreach (string fileInDir in Directory.GetFiles(file))
                                    {
                                        links = FileReader.ExtractLinks(fileInDir);
                                        Console.Write("===|Reading file : ");
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"{fileInDir}|===\n");
                                        Console.ResetColor();
                                        wlc.DisplayLinks(links, args);
                                    }
                                }
                                else
                                {
                                    Console.Write("===|Reading file : ");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"{file}|===\n");
                                    Console.ResetColor();
                                    wlc.DisplayLinks(links, args);

                                }
                            }


                            else
                                Console.WriteLine("No such file or url exist...\n");
                        }
                    }
                    Console.WriteLine($"Good links: {WebLinkChecker.GetGoodCounter()} | Bad links: {WebLinkChecker.GetBadCounter()} | Unknown links: {WebLinkChecker.GetUnknownCounter()}");
                }
            }
        }
    }
}
