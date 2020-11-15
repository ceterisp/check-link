using CheckLinkCLI2.General;
using CheckLinkCLI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CheckLinkCLI2
{
    public class WebLinkChecker
    {
        private static long goodCounter, badCounter, unknownCounter = 0;
        private readonly FileReader fr = new FileReader();
        private readonly List<string> supportFlags = new List<string>() { "--all", "--good", "--bad" };

        /// <summary>
        /// Returns total number of bad links
        /// </summary>
        /// <returns></returns>
        public static long GetBadCounter()
        {
            return badCounter;
        }

        /// <summary>
        /// Returns total number of good links
        /// </summary>
        /// <returns></returns>
        public static long GetGoodCounter()
        {
            return goodCounter;
        }

        /// <summary>
        /// Returns total number of unknown links
        /// </summary>
        /// <returns></returns>
        public static long GetUnknownCounter()
        {
            return unknownCounter;
        }

        /// <summary>
        /// Prints each link, status and the code to the console
        /// </summary>
        /// <param name="links"> The links after parsed from the file</param>
        /// <param name="args">The file path</param>
        public void DisplayLinks(List<string> links, string[] args)
        {
            links.Sort();
            foreach (var link in links)
            {
                string flag = SetSupportFlag(args.Last<string>()) ?? "--all";
                GetAllEndPointWithUri(link, flag);
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Prints Good and Bad link to console
        /// </summary>
        /// <param name="url"></param>
        public void GetAllEndPointWithUri(string url, string supportFlag = "--all")
        {
            HttpClient httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(2.5)
            };
            int? statusCode;
            try
            {
                int count = 0;
                Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, new Uri(url)));
                HttpResponseMessage httpResponseMessage = httpResponse.Result;
                HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
                statusCode = (int)httpStatusCode;
                httpClient.Dispose();
                Link link = new Link();
                if (statusCode == 200 && supportFlag != "--bad")
                {
                    link.Id = count++;
                    link.StatusCode = statusCode; // adding the statuscode inside the object
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[{statusCode}] ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{url} ");
                    Console.Write(" : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Good");
                    goodCounter++;
                }
                else if ((statusCode == 400 || statusCode == 404) && supportFlag != "--good")
                {
                    link.Id = count++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"[{statusCode}] ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{url} ");
                    Console.Write(" : ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bad");
                    badCounter++;
                }
                else if (statusCode != null && statusCode != 400 && statusCode != 404 && statusCode != 200 && supportFlag == "--all")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[{statusCode}] ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{url} ");
                    Console.Write(" : ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Unknown");
                    unknownCounter++;
                }
                Console.ResetColor();
            }
            catch (Exception e)
            {
                if (e.InnerException.Message == "A task was canceled." && supportFlag != "--good")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[404] ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{url} ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(": Bad (Timeout)");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    badCounter++;
                }
                else
                {
                    if (supportFlag == "--all")
                    {
                        Console.Write("[UKN] ");
                        Console.Write($"{url} ");
                        Console.WriteLine(": Unknown");
                        unknownCounter++;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a List of Link objects and its properties
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public List<Link> GetLinkDetails(string file)
        {
            int count = 0;
            int? statusCode;
            List<Link> link = new List<Link>();
            int totalFiles = fr.ExtractLinks(file).Count;
            Console.WriteLine("\nAdding link results to JSON file...\n");
            foreach (var parselink in fr.ExtractLinks(file))
            {
                try
                {
                    HttpClient httpClient = new HttpClient
                    {
                        Timeout = TimeSpan.FromSeconds(2.5)
                    };
                    Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(parselink);
                    HttpResponseMessage httpResponseMessage = httpResponse.Result;
                    HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
                    statusCode = (int)httpStatusCode;
                    httpClient.Dispose();
                    var l = new Link()
                    {
                        Id = count++,
                        Url = parselink,
                        StatusCode = statusCode,
                        LinkStatus = statusCode == 200 ? "Good" : "Bad"
                    };

                    link.Add(l);
                    Utility.ProgressBar(count, totalFiles);
                }
                catch (Exception)
                {
                    var l = new Link()
                    {
                        Id = count++,
                        Url = parselink,
                        StatusCode = 000,
                        LinkStatus = "Unknown"
                    };

                    link.Add(l);
                    Utility.ProgressBar(count, totalFiles);
                }
            }

            return link;
        }

        /// <summary>
        /// Returns Support Flag value for JSON
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string SetSupportFlag(string flag)
        {
            if (supportFlags.Contains(flag))
                return supportFlags[supportFlags.IndexOf(flag)];
            else
                return supportFlags[0];
        }
    }
}