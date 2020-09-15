using System;
using System.Security.Cryptography.X509Certificates;

namespace CheckLinkCLI
{
    public class Program
    {
        public static string file;
        FileReader FileReader = new FileReader(file);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
