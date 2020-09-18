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
    public class MainBrokenLink
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
        public static readonly List<string> version = new List<string>() { "v", "-v", "version", "--version" };
        public static Dictionary<string, List<string>> CommandLineOptions = new Dictionary<string, List<string>>()
        {
            {"version",version }
        };
        //{
        //     { "version", ["v","-v","version","--version"] },

        //}


        public static void Main(string[] args)
        {
            FileReader FileReader = new FileReader();
            WebLinkChecker LinkChecker = new WebLinkChecker();

            if (IsDebug())
            {
                //TODO: Search function that checks only one or few links on-demand

                //Type type = typeof(ConsoleColor);
                //Console.ForegroundColor = ConsoleColor.White;
                //foreach (var name in Enum.GetNames(type))
                //{
                //    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, name);
                //    Console.WriteLine(name);
                //}
                //Console.BackgroundColor = ConsoleColor.Black;
                //foreach (var name in Enum.GetNames(type))
                //{
                //    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, name);
                //    Console.WriteLine(name);
                //}

                var links = FileReader.ExtractLinks(linkFile);
                foreach (var link in links)
                {
                    LinkChecker.GetAllEndPointWithUri(link);
                }
            }

            else
            {
                #region Command line options
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "v" || args[i] == "-v" || args[i] == "version" || args[i] == "--version")
                    {
                        Console.WriteLine("Application Name: CheckLinkCLI2 \n" +
                            "Release: 0.1");
                    }
                }
                #endregion

                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide file name with links as an argument...");
                    Console.WriteLine("For example: CheckLinksCLI2 file_name.txt");

                }

                else
                {
                    var links = FileReader.ExtractLinks(args[0]);
                    foreach (var link in links)
                    {
                        LinkChecker.GetAllEndPointWithUri(link);
                    }
                }
            }


        }





    }

}
