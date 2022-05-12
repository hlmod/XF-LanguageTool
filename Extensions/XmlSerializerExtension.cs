// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using System.Xml.Serialization;

namespace HLmod.XenForo.LanguageTool.Extensions;

public static class XmlSerializerExtension
{
    private static XmlSerializerNamespaces namespaces
    {
        get
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            return ns;
        }
    }

    /// <summary>
    /// Performs serialization operation without namespaces.
    /// </summary>
    /// <param name="xmlSerializer"></param>
    /// <param name="stream"></param>
    /// <param name="o"></param>
    public static void SerializeWithoutNamespaces(this XmlSerializer xmlSerializer, Stream stream, object o)
    {
        xmlSerializer.Serialize(stream, o, namespaces);
    }
}
