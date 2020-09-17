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

namespace CheckLinkCLI2
{
    public class MainBrokenLink
    {
        private static readonly string linkFile = @"D:\Documents\Seneca College\OSD600\check-link\check-link\CheckLinkCLI2\CheckLinkCLI2\links.txt";

        static void Main(string[] args)
        {
            //var statusCode = (int)MainBrokenLink.GetHttpStatusCode("https://duckduckgo.m");
            //var statusCode = GetAllEndPointWithUri("https://duckduckgo.m");
            //Console.WriteLine($"The status code of the link is {GetAllEndPointWithUri("https://duckduckgo.com")}");
            FileReader FileReader = new FileReader();
            WebLinkChecker LinkChecker = new WebLinkChecker();

            var links = FileReader.ExtractLinks(linkFile);

            foreach (var link in links)
            {
                LinkChecker.GetAllEndPointWithUri(link);
            }

        }

    }

}
