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
        //public static readonly List<string> json = new List<string>() { }
        public static readonly List<string> supportFlags = new List<string>() { "--all", "--good", "--bad" };
        //public static readonly Dictionary<int, string> supportFlags = new Dictionary<int, string>()
        //{
        //    { 0, "--all"},
        //    { 1, "--good"},
        //    { 2, "--bad"}
        //};
        public static Dictionary<string, List<string>> CommandLineOptions = new Dictionary<string, List<string>>()
        {
            {"version",version },
            //{"flag", supportFlags }
        };

        public static void Main(string[] args)
        {
            FileReader FileReader = new FileReader();
            WebLinkChecker LinkChecker = new WebLinkChecker();

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
                        foreach (var link in FileReader.ExtractLinks(htmlFile))
                        {
                            string flag = LinkChecker.SetSupportFlag(args.Last<string>()) == null ? "--all" : LinkChecker.SetSupportFlag(args.Last<string>());
                            LinkChecker.GetAllEndPointWithUri(link,flag);
                        }
                        break;
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
                                // TODO: add the support flags here

                                Console.Write("===|Reading file : ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"{file}|===\n");
                                Console.ResetColor();
                                var links = FileReader.ExtractLinks(file);
                                links.Sort();
                                foreach (var link in links)
                                {
                                    string flag = LinkChecker.SetSupportFlag(args.Last<string>()) == null ? "--all" : LinkChecker.SetSupportFlag(args.Last<string>());
                                    LinkChecker.GetAllEndPointWithUri(link, flag);
                                }

                                Console.WriteLine("\n");
                            }

                            else if (Directory.Exists(file))
                            {
                                Console.Write("===|Reading all files in directory : ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"{file}|===\n");
                                Console.ResetColor();
                                string[] files = Directory.GetFiles(file);
                                foreach (string fileInDir in files)
                                {
                                    var links = FileReader.ExtractLinks(fileInDir);
                                    Console.Write("===|Reading file : ");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine($"{fileInDir}|===\n");
                                    Console.ResetColor();
                                    links.Sort();
                                    foreach (var link in links)
                                    {
                                        string flag = LinkChecker.SetSupportFlag(args.Last<string>()) == null ? "--all" : LinkChecker.SetSupportFlag(args.Last<string>());
                                        LinkChecker.GetAllEndPointWithUri(link, flag);
                                    }

                                    Console.WriteLine("\n");

                                }

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
