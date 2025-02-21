using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAutomationTests
{
    public class ApiTests : BaseTest
    {
        [Test]
        public void ReadXml()
        {
            var stringData = EmbeddedFileReader.ReadAndReplaceNodeInXml("Sample.xml", "ApplicationName", "ApiTest");
            Console.WriteLine(stringData);
        }
    }
}
