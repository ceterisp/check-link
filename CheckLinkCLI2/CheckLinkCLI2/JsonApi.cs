using CheckLinkCLI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace CheckLinkCLI2
{
    public class JsonApi
    {
        //private Telescope telescope = new Telescope();
        private const string telescopePostsUrl = @"http://localhost:3000/posts";
        private WebLinkChecker webChecker = new WebLinkChecker();

        public void ParseLinks()
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
                List<string> links = new List<string>();

                for (int i = 0; i < 10; i++)
                {
                    links.Add($"{telescopePostsUrl}{posts[i].Url}");
                    //Console.WriteLine(posts[i]);
                }

                foreach (var link in links)
                {
                    webChecker.GetAllEndPointWithUri(link);
                }
                //Console.WriteLine("Want to know more about a specifc elephant? (Type: Balarama)");
                //string n = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
