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
using System.Linq;

namespace CheckLinkCLI2
{
    public class MainBrokenLink
    {
        #region Debug or Release
        public static bool IsDebug()
        {
#if (DEBUG)
            return true;
#else
            return false;
#endif
        }

        #endregion

        private static readonly string linkFile = @"D:\Documents\Seneca College\OSD600\check-link\check-link\CheckLinkCLI2\CheckLinkCLI2\urls.txt";

        public static void Main(string[] args)
        {
            FileReader FileReader = new FileReader();
            WebLinkChecker LinkChecker = new WebLinkChecker();

            //this is a test comment
            if (IsDebug())
            {
                var links = FileReader.ExtractLinks(linkFile);
                foreach (var link in links)
                {
                    LinkChecker.GetAllEndPointWithUri(link);
                }
            }

            else
            {
                if (args.Length != 1 || args.Length > 1)
                {
                    Console.WriteLine("Please provide file name with links as an argument...");
                }

                else
                {
                    var links = FileReader.ExtractLinks(args[0]);
                    foreach (var link in links)
                    {
                        LinkChecker.GetAllEndPointWithUri(link);
                    }
                }
            }


        }





    }

}
