using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace DbAutomationTests
{
    public class EmbeddedFileReader
    {
        private readonly EmbeddedFileProvider _embeddedFileProvider;

        public EmbeddedFileReader()
        {
            _embeddedFileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly()) ?? throw new ArgumentNullException(nameof(EmbeddedFileProvider));
        }

        public byte[] ReadAsBytes(string fileName)
        {
            var fileInfo = _embeddedFileProvider.GetFileInfo($"TestData.{fileName}");
            if (!fileInfo.Exists)
            {
                throw new ArgumentException($"File not found: {fileName} in TestData directory");
            }
            using var stream = fileInfo.CreateReadStream();
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public string ReadAsString(string fileName)
        {
            var fileInfo = _embeddedFileProvider.GetFileInfo($"TestData.{fileName}");
            if (!fileInfo.Exists)
            {
                throw new ArgumentException($"File not found: {fileName} in TestData directory");
            }
            using var stream = fileInfo.CreateReadStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public IFileInfo GetFileInfo(string fileName) => _embeddedFileProvider.GetFileInfo($"TestData.{fileName}");

        //public string ReadAndReplaceNodeInXml(string fileName, string node, string value)
        //{
        //    var fileInfo = GetFileInfo(fileName);
        //    if (!fileInfo.Exists)
        //    {
        //        throw new ArgumentException($"File not found: {fileName}");
        //    }

        //    if(!IsXmlFile(fileName))
        //    {
        //        throw new ArgumentException($"File is not an XML file: {fileName}");
        //    }

        //    var xml = ReadAsString(fileName);
        //    var newXml = xml.Replace($"<{node}>", $"<{node}>{value}");

        //    return newXml;
        //}

        public string ReadAndReplaceNodeInXml(string fileName, string node, string value)
        {
            var fileInfo = GetFileInfo(fileName);
            if (!fileInfo.Exists)
            {
                throw new ArgumentException($"File not found: {fileName}");
            }

            if (!IsXmlFile(fileName))
            {
                throw new ArgumentException($"File is not an XML file: {fileName}");
            }

            var xml = ReadAsString(fileName); 
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var targetNode = xmlDoc.SelectSingleNode($"//{node}") ?? throw new ArgumentException($"Node not found: {node}");

            targetNode.InnerText = value;

            using var stringWriter = new StringWriter();
            using var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
            xmlDoc.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }

        public bool IsXmlFile(string fileName)
        {
            try
            {
                var xmlContent = ReadAsString(fileName);
                XDocument.Parse(xmlContent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
