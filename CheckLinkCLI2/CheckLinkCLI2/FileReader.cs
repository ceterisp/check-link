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

            if (File.Exists(file))
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
                //string text = File.ReadAllText(file);
                //Console.WriteLine($"The text inside the file is {file}");
            }
            return links;
        }
    }
}
