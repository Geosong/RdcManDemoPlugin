namespace RdcManDemoPlugin.Models
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Interfaces.Models;

    [Serializable]
    [XmlRoot("Settings")]
    public class PluginSettings : IPluginSettings
    {
        public string LastSave { get; set; }

        public PluginSettings()
        {
            LastSave = string.Empty;
        }

        public static IPluginSettings CreateSettings(XmlNode xmlNode)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PluginSettings));
            XmlNodeReader reader = new XmlNodeReader(xmlNode);
            return (IPluginSettings)serializer.Deserialize(reader);
        }

        public XmlNode ToNode()
        {
            StringBuilder stringBuilder = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(typeof(PluginSettings));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            XmlWriterSettings writerSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true
            };

            namespaces.Add(string.Empty, string.Empty);

            XmlWriter writer = XmlWriter.Create(stringBuilder, writerSettings);
            serializer.Serialize(writer, this, namespaces);
            doc.LoadXml(stringBuilder.ToString());
            return doc;
        }
    }
}