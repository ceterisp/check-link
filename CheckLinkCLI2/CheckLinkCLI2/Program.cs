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

        private static readonly string linkFile = @"D:\Documents\Seneca College\OSD600\check-link\check-link\CheckLinkCLI2\CheckLinkCLI2\urls.txt";
        private static readonly string htmlFile = @"D:\Documents\Seneca College\OSD600\check-link2\check-link\CheckLinkCLI2\CheckLinkCLI2\index2.html";
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

                //var links = FileReader.ExtractLinks(htmlFile);
                //foreach (var link in links)
                //{
                //    LinkChecker.GetAllEndPointWithUri(link);
                //}

                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide file name with links as an argument...");
                    Console.WriteLine("For example: CheckLinksCLI2 file_name.txt");
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
                        if (file.StartsWith("http") || file.StartsWith("https"))
                        {
                            LinkChecker.GetAllEndPointWithUri(file);
                            Console.WriteLine("\n");
                        }

                        else
                        {
                            Console.Write("===|Reading file : ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{file}|===\n");
                            Console.ResetColor();
                            var links = FileReader.ExtractLinks(file);
                            foreach (var link in links)
                            {
                                LinkChecker.GetAllEndPointWithUri(link);
                            }

                            Console.WriteLine("\n");

                        }
                    }

                    Console.WriteLine($"Good links: {WebLinkChecker.goodCounter} | Bad links: {WebLinkChecker.badCounter} | Unknown links: {WebLinkChecker.unknownCounter}");
                }
            }


        }





    }

}
