using CheckLinkCLI2.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace CheckLinkCLI2
{
    public class JsonApi
    {
        private List<string> extractedLinks = new List<string>();

        //private Telescope telescope = new Telescope();
        private const string telescopePostsUrl = @"http://localhost:3000/posts";

        private WebLinkChecker webChecker = new WebLinkChecker();

        /// <summary>
        /// Parses the links from the JSON, extracts the link and returns the http statuses to the console
        /// </summary>
        public void ParseLinksFromJson()
        {
            string content = string.Empty;
            using (var wc = new WebClient())
            {
                content = wc.DownloadString(telescopePostsUrl);
            }

            List<Telescope> posts = new List<Telescope>();

            try
            {
                posts = JsonSerializer.Deserialize<List<Telescope>>(content);

                Random random = new Random();
                int randomNumber = random.Next(0, 10);
                List<string> jsonlinks = new List<string>();
                List<string> links = new List<string>();

                for (int i = 0; i < 10; i++)
                {
                    jsonlinks.Add($"{telescopePostsUrl}/{posts[i].Id}");
                }

                foreach (var link in jsonlinks)
                {
                    ExtractLinksFromHtml(link);
                }

                foreach (var link in extractedLinks)
                {
                    webChecker.GetAllEndPointWithUri(link);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Extracts links from the html file and stores them in a list
        /// </summary>
        /// <param name="htmllink"></param>
        private void ExtractLinksFromHtml(string htmllink)
        {
            string htmlcontent = string.Empty;
            using (var wc = new WebClient())
            {
                htmlcontent = wc.DownloadString(htmllink);
            }

            try
            {
                foreach (var i in htmlcontent.Replace("\\", string.Empty).Split("\""))
                {
                    if (i.StartsWith("http"))
                    {
                        string trimmedLink = i.Replace(@"\\", string.Empty);
                        extractedLinks.Add(trimmedLink);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}