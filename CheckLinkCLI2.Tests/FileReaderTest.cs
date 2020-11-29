using System.IO;
using System.Threading;
using Xunit;

namespace CheckLinkCLI2.Tests
{
    public class FileReaderTest
    {
        private readonly string _txtFile = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/urls.txt";
        private readonly string _htmlFile = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/urls.html";
        private readonly string _noFile = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/nofile.txt";
        private readonly string _ignoreFile = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/ignore_test.txt";
        private readonly string _ignoreFileShould = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/ignore_should.txt";
        private readonly string _jsonOutput = @$"{Directory.GetCurrentDirectory()}/../../../obj/Debug/netcoreapp3.1/CheckLinkCLI2JsonOutput_should.json";
        private readonly string _jsonOutputShould = @$"{Directory.GetCurrentDirectory()}/../../../assets_test/CheckLinkCLI2JsonOutput_should.json";

        private readonly FileReader fr = new FileReader();

        [Fact]
        public void ExtractLinkFromTxtFileShould()
        {
            //Arrange

            //Act
            var links = fr.ExtractLinks(_txtFile);

            //Assert
            var sr = new StreamReader(_txtFile);

            foreach (var link in links)
            {
                var linkInLine = sr.ReadLine();
                Assert.Equal(linkInLine, link);
            }
        }

        [Fact]
        public void ExtractLinkFromHtmlFileShould()
        {
            //Arrange

            //Act
            var links = fr.ExtractLinks(_htmlFile);

            //Assert
            var sr = new StreamReader(_txtFile);

            foreach (var link in links)
            {
                var linkInLine = sr.ReadLine();
                Assert.Equal(linkInLine, link);
            }
        }

        [Fact]
        public void ExtractLinkWhenFileNotFoundShould()
        {
            //Arrange

            //Act
            var links = fr.ExtractLinks(_noFile);

            //Assert
            string sr = "No file found!";

            foreach (var link in links)
            {
                Assert.Equal(sr, link);
            }
        }

        [Fact]
        public void ReadIgnorePatternsFromTxtFileShould()
        {
            //Arrange

            //Act
            var ignoredLinks = fr.ReadIgnorePatterns(_ignoreFile);
            //var ignoredLink = ignoredLinks[0];
            //Assert
            var sr = new StreamReader(_ignoreFileShould);
            foreach (var link in ignoredLinks)
            {
                string dLinks = sr.ReadLine();
                Assert.Equal(dLinks, link);
            }
        }

        [Fact(Skip = "Ready to test by someone else")]
        public void ReadIgnorePatternsFromNoFileShould() // This use case causes the System to exit, therefore test can't run
        {
            //Arrange

            //Act
            var ignoredLinks = fr.ReadIgnorePatterns(_noFile);
            var ignoredLink = ignoredLinks[0];
            //Assert
            var sr = new StreamReader(_ignoreFileShould);
            string dLinks = sr.ReadLine();
            Assert.Equal(dLinks, ignoredLink);
        }

        [Fact(Skip = "Calls ProgressBar that prints to console, doesn't return value")]
        public void WriteToJsonFromHtmlFileShould() // not sure how to write test for this use case
        {
            fr.WriteToJSON(_txtFile);
            Thread.Sleep(180000);
            //Task.WaitAll()
            //Task.WaitAll(fr.WriteToJSON(_txtFile),)
            Assert.Equal(_jsonOutputShould, _jsonOutput);
        }
    }
}