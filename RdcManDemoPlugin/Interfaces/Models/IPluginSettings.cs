namespace RdcManDemoPlugin.Interfaces.Models
{
    using System.Xml;

    public interface IPluginSettings
    {
        string LastSave { get; set; }

        XmlNode ToNode();
    }
}