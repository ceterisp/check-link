using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using CheckLinkCLI2.Models;
using Xunit;

namespace CheckLinkCLI2.Tests
{
    public class WebLinkCheckerTest
    {
        private readonly string _txtFile = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/urls.txt";

        private readonly WebLinkChecker wlc = new WebLinkChecker();

        [Fact(Skip = "Calls ProgressBar that prints to console, doesn't return value")]
        public void GetLinkDetailsWithFileShould()
        {
            //Arrange
            var good = new Link()
            {
                Id = 1,
                Url = "https://wiki.cdot.senecacollege.ca/w/api.php?action=rsd",
                StatusCode = 200,
                LinkStatus = "Good"
            };
            var bad = new Link()
            {
                Id = 2,
                Url = "http://en.wikipedia.org/wiki/Hackergotchi",
                StatusCode = 404,
                LinkStatus = "Bad"
            };
            var unknown = new Link()
            {
                Id = 3,
                Url = "http://s-aleinikov.blog.ca/feed/atom/posts/",
                StatusCode = 000,
                LinkStatus = "Unknown"
            };
            var unknown2 = new Link()
            {
                Id = 4,
                Url = "http://www.danepstein.ca/category/open-source/feed",
                StatusCode = 000,
                LinkStatus = "Unknown"
            };
            //Act
            var links = wlc.GetLinkDetails(_txtFile);

            // Assert
            Assert.Equal(good, links[0]);
            Assert.Equal(bad, links[1]);
            Assert.Equal(unknown, links[2]);
            Assert.Equal(unknown2, links[3]);
        }

        [Fact]
        public void SetSupportFlagForAllShould()
        {
            //Arrange
            var flag = wlc.SetSupportFlag("--all");

            //Assert
            Assert.Equal("--all", flag);
        }

        [Fact]
        public void SetSupportFlagForBadShould()
        {
            //Arrange
            var flag = wlc.SetSupportFlag("--bad");

            //Assert
            Assert.Equal("--bad", flag);
        }

        [Fact]
        public void SetSupportFlagForGoodShould()
        {
            //Arrange
            var flag = wlc.SetSupportFlag("--good");

            //Assert
            Assert.Equal("--good", flag);
        }

        [Fact]
        public void SetSupportFlagForRandomShould()
        {
            //Arrange
            var flag = wlc.SetSupportFlag("anythingrandomyoucanthink of");

            //Assert
            Assert.Equal("--all", flag);
        }

        [Fact(Skip = "Prints to the console, doesn't return value")]
        public void GetAllEndPointWithUriWithAllShould() // also hard to
        {
            wlc.GetAllEndPointWithUri("https://wiki.cdot.senecacollege.ca/w/api.php?action=rsd");
        }

        [Fact]
        public void GetHttpStatusCodeForGoodUrlShould()
        {
            //Arrange
            HttpClient httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(2.5)
            };

            int statuscode = wlc.GetHttpStatusCode(httpClient, "http://google.com");

            //Assert
            Assert.Equal(200, statuscode);
        }

        [Fact]
        public void GetHttpStatusCodeForBadUrlShould()
        {
            //Arrange
            HttpClient httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(2.5)
            };

            int statuscode = wlc.GetHttpStatusCode(httpClient, "https://www.google.com/somethingrandom");

            //Assert
            Assert.Equal(404, statuscode);
        }
    }
}