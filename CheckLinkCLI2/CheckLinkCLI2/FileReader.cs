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
                                string[] l = link.Split("\"");
                                links.Add(l[1]);
                            }
                        }

                        else
                        {
                            links.Add(sr.ReadLine());
                        }
                    }
                    sr.Close();
                }
            }
            return links;
        }

        private bool IsCommandLineOption(List<string> commandLineOption, string fileArg)
        {
            foreach (var clo in commandLineOption)
            {
                if (fileArg.Contains(clo))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsHtmlFile(string html)
        {
            if (html.Contains(".html"))
            {
                return true;
            }

            return false;

        }

    }
}
