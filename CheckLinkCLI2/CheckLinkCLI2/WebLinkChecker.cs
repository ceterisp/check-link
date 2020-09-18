using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CheckLinkCLI2
{
    public class WebLinkChecker
    {
        //initializing the default status code, which is always 0
        private HttpStatusCode results = default(HttpStatusCode);

        /// <summary>
        /// Prints Good and Bad link to console
        /// </summary>
        /// <param name="url"></param>
        public void GetAllEndPointWithUri(string url)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(url);
                HttpResponseMessage httpResponseMessage = httpResponse.Result;
                //Console.WriteLine(httpResponseMessage.ToString());
                HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
                httpClient.Dispose();
                if ((int)httpStatusCode == 200)
                {
                    Console.Write($"{url} : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Good");

                }
                else if ((int)httpStatusCode == 400 || (int)httpStatusCode == 404)
                {
                    Console.Write($"{url} : ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bad");
                }
                Console.ResetColor();
            }
            catch (Exception)
            {

                Console.WriteLine($"{url} : Unknown");

            }

        }

        /// <summary>
        /// Only checks for web links that return a valid HTTP status code
        /// </summary>
        public HttpStatusCode GetHttpStatusCode(string url)
        {
            // Creating a HttpWebRequest
            var request = HttpWebRequest.Create(url);

            //Setting the Request method HEAD
            request.Method = "HEAD";

            //try while we are getting a response

            try
            {
                var response = request.GetResponse() as HttpWebResponse;

                if (response != null)
                {
                    results = response.StatusCode;
                    response.Close();
                }
            }
            catch (Exception)
            {
                //response.Close();
                return results;
            }
            return results;

        }



        ///
        /// Checks the file exists or not.
        ///
        /// The URL of the remote file.
        /// True : If the file exits, False if file not exists
        //private bool RemoteFileExists(string url)
        //{
        //    try
        //    {
        //        //Creating the HttpWebRequest
        //        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //        //Setting the Request method HEAD, you can also use GET too.
        //        request.Method = "HEAD";
        //        //Getting the Web Response.
        //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //        //Returns TRUE if the Status code == 200
        //        response.Close();
        //        return (response.StatusCode == HttpStatusCode.OK);
        //    }
        //    catch
        //    {
        //        //Any exception will returns false.
        //        return false;
        //    }
        //}
    }
}
