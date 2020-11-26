using CheckLinkCLI2.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace CheckLinkCLI2
{
    public class JsonApi
    {
        private List<string> _extractedLinks = new List<string>();

        //private Telescope telescope = new Telescope();
        //private const string telescopePostsUrl = @"http://localhost:3000/posts";
        private const string telescopePostsUrl = @"https://telescope.cdot.systems/posts";

        private WebLinkChecker _webChecker = new WebLinkChecker();

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

                foreach (var post in posts)
                {
                    jsonlinks.Add($"{telescopePostsUrl}/{post.Id}");
                }

                if (!IsHtmlFile(jsonlinks[0]))
                {
                    //List<TelescopePosts> telescopePost = new List<TelescopePosts>();
                    //List<TelescopePosts> telescopePosts = JsonSerializer.Deserialize<List<TelescopePosts>>(jsonlinks);

                    foreach (var jsonlink in jsonlinks)
                    {
                        using (var wc = new WebClient())
                        {
                            content = wc.DownloadString(jsonlink);
                        }

                        TelescopePosts telescopePosts = JsonSerializer.Deserialize<TelescopePosts>(content);
                        ExtractLinksFromHtml(telescopePosts.Html);
                    }
                }
                else
                {
                    if (_extractedLinks.Count == 0)
                    {
                        foreach (var link in jsonlinks)
                        {
                            ExtractLinksFromHtml(link);
                        }
                    }

                    foreach (var link in _extractedLinks)
                    {
                        _webChecker.GetAllEndPointWithUri(link);
                    }
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
            string htmlcontent = htmllink;
            if (!htmllink.StartsWith("<"))
            {
                using (var wc = new WebClient())
                {
                    htmlcontent = wc.DownloadString(htmllink);
                }
            }

            try
            {
                foreach (var i in htmlcontent.Replace("\\", string.Empty).Split("\""))
                {
                    if (i.StartsWith("http"))
                    {
                        string trimmedLink = i.Replace(@"\\", string.Empty);
                        _extractedLinks.Add(trimmedLink);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private bool IsJsonFile(string json)
        {
            return json.Contains(".json") ? true : false;
        }

        private bool IsHtmlFile(string html)
        {
            return html.StartsWith("<!DOCTYPE html>") ? true : false;
        }
    }
}