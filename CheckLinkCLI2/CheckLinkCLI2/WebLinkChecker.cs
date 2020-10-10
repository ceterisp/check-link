using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CheckLinkCLI2.Models;
using CheckLinkCLI2.General;

namespace CheckLinkCLI2
{
    public class WebLinkChecker
    {
        //initializing the default status code, which is always 0
        //private HttpStatusCode results = default(HttpStatusCode);
        public static long goodCounter, badCounter, unknownCounter = 0;
        FileReader fr = new FileReader();
        
        /// <summary>
        /// Prints Good and Bad link to console
        /// </summary>
        /// <param name="url"></param>
        public void GetAllEndPointWithUri(string url, string supportFlag = "--all")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(2.5);
            int? statusCode;
            try
            {
                int count = 0;

                //TODO:
                //add support for timeouts, DNS resolution issues, or other server errors when accessing a bad URL. A bad domain, URL, or server shouldn't crash your tool.
                //add a command line flag:
                //1. to allow specifying a custom User Agent string when doing network requests
                //2. to allow checking for archived versions of URLs using the WayBackMachine
                //3. to allow checking whether http:// URLs actually work using https://
                //4. add support for parallelization, using multiple CPU cores so your program can do more than one thing at a time
                //

                //Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(url);
                Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, new Uri(url)));
                HttpResponseMessage httpResponseMessage = httpResponse.Result;
                HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
                statusCode = (int)httpStatusCode;
                httpClient.Dispose();
                if (statusCode == 200 && supportFlag != "--bad")
                {
                    Link link = new Link();
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
                    Link link = new Link();
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
                    //Console.Write($"[{statusCode}] : ");
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
                        //Console.Write($"[{statusCode}] : ");
                        Console.WriteLine(": Unknown");
                        unknownCounter++;
                    }
                }
            }
        }

        /// <summary>
        /// Returns Support Flag value for JSON 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string SetSupportFlag(string flag)
        {
            if (Program.supportFlags.Contains(flag))
                return Program.supportFlags[Program.supportFlags.IndexOf(flag)];
            else
                return Program.supportFlags[0];
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
            var link = new List<Link>();
            int totalFiles = fr.ExtractLinks(file).Count;
            Console.WriteLine("\nAdding link results to JSON file...\n");
            foreach (var parselink in fr.ExtractLinks(file))
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = TimeSpan.FromSeconds(2.5);
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
                catch (Exception e)
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
    }
}
