using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CheckLinkCLI2
{
    public class FileReader
    {
        public List<string> ExtractLinks(string file)
        {
            List<string> links = new List<string>();

            if (File.Exists(file) && !IsCommandLineOption(MainBrokenLink.version, file))
            {
                //read the file line by line
                using (StreamReader sr = new StreamReader(file))
                {
                    int counter = 0;
                    string ln;
                    while (!sr.EndOfStream)
                    {
                        links.Add(sr.ReadLine());
                        counter++;
                    }
                    sr.Close();
                }
            }

            //else if(!File.Exists(file))
            //{
            //    Console.WriteLine($"No such file name {file} exists.");
            //}

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
    }
}
