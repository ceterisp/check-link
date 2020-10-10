using CheckLinkCLI2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace CheckLinkCLI2
{
    public class FileReader
    {
        public List<string> ExtractLinks(string file)
        {
            List<string> links = new List<string>();

            if (File.Exists(file) && !IsCommandLineOption(Program.version, file))
            {
                //read the file line by line
                using (StreamReader sr = new StreamReader(file))
                {
                    int counter = 0;
                    while (!sr.EndOfStream)
                    {
                        if (IsHtmlFile(file))
                        {
                            List<string> htmlLine = new List<string>();
                            htmlLine.Add(sr.ReadLine());
                            foreach (var link in htmlLine)
                            {
                                foreach (var i in link.Split("\""))
                                {
                                    if (i.StartsWith("http"))
                                        links.Add(i);
                                }
                            }
                        }
                        else
                            links.Add(sr.ReadLine());
                    }
                    sr.Close();
                }
            }
            else
                links.Add("No file found!");

            return links;
        }

        private bool IsCommandLineOption(List<string> commandLineOption, string fileArg)
        {
            foreach (var clo in commandLineOption)
            {
                if (fileArg.Contains(clo))
                    return true;
            }
            return false;
        }

        private bool IsHtmlFile(string html)
        {
            if (html.Contains(".html"))
                return true;

            return false;

        }
        /// <summary>
        /// Receives file and writes it to a JSON file
        /// </summary>
        /// <param name="url"></param>
        public void WriteToJSON(string file)
        {
            const string fileName = @"\CheckLinkCLI2JsonOutput.json";
            WebLinkChecker lc = new WebLinkChecker();
            var link = lc.GetLinkDetails(file);
            var jsonToWrite = JsonConvert.SerializeObject(link, Formatting.Indented);
            string path = Directory.GetCurrentDirectory() + fileName;
            using (var writer = new StreamWriter(path))
            {
                writer.Write(jsonToWrite);
            }
        }
    }
}
