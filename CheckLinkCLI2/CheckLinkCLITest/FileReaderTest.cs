using CheckLinkCLI2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Xunit;

namespace CheckLinkCLITest
{
    public class FileReaderTest
    {
        [Fact]
        public void ExtractLinkShould()
        {
            string file = "D:\\Documents\\Seneca College\\OSD600\\check-link\\check-link\\CheckLinkCLI2\\CheckLinkCLI2\\links.txt";
            //Arrange
            FileReader fr = new FileReader();

            //Act
            List<string> links = fr.ExtractLinks(file);

            //Assert
            StreamReader sr = new StreamReader(file);
            string linkInLine;
            foreach (var link in links)
            {
                linkInLine = sr.ReadLine();
                Assert.Equal(linkInLine , link);
            }

        }
    }
}
