using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
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

        private static readonly string linkFile = "C:\\Users\\joel_\\Documents\\school\\Fall 2020\\OSD600\\lab 2\\testFiles\\index.txt";
        private static readonly string htmlFile = "C:\\Users\\joel_\\Documents\\school\\Fall 2020\\OSD600\\lab 2\\testFiles\\index3.html";
        public static readonly List<string> version = new List<string>() { "v", "-v", "version", "--version" };
        public static Dictionary<string, List<string>> CommandLineOptions = new Dictionary<string, List<string>>()
        {
            {"version",version }
        };

        public static void Main(string[] args)
        {
            FileReader FileReader = new FileReader();
            WebLinkChecker LinkChecker = new WebLinkChecker();

            #region Dev env
            if (IsDebug())
            {
                //TODO: Search function that checks only one or few links on-demand

                foreach (var input in args)
                {
                    if (!File.Exists(input) && input.Contains(':') && !(input.EndsWith(".txt") || input.EndsWith(".html")) && input.StartsWith("http"))
                    {
                        LinkChecker.GetAllEndPointWithUri(input);
                        Console.WriteLine("\n");
                    }

                    else if (File.Exists(input))
                    {
                        var links = FileReader.ExtractLinks(htmlFile);
                        foreach (var link in links)
                        {
                            LinkChecker.GetAllEndPointWithUri(link);
                        }
                    }

                    else
                    {
                        Console.WriteLine($"File {input} does not exist");
                    }
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

                #region Command line options
                else if (version.Contains(args[0]))
                {
                    Console.WriteLine("Application Name: CheckLinkCLI2 \n" +
                        "Release: 0.1");
                }
                #endregion

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

                        else
                        {
                            if (File.Exists(file))
                            {
                                Console.Write("===|Reading file : ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"{file}|===\n");
                                Console.ResetColor();
                                var links = FileReader.ExtractLinks(file);
                                links.Sort();
                                foreach (var link in links)
                                {
                                    LinkChecker.GetAllEndPointWithUri(link);
                                }

                                Console.WriteLine("\n");
                            }

                            else
                            {
                                Console.WriteLine("No such file or url exist...\n");
                            }
                        }

                    }
                    Console.WriteLine($"Good links: {WebLinkChecker.goodCounter} | Bad links: {WebLinkChecker.badCounter} | Unknown links: {WebLinkChecker.unknownCounter}");
                }
            }
        }
    }
}
