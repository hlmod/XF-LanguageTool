// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using System.Xml;
using System.Xml.Serialization;

namespace HLmod.XenForo.LanguageTool.Models;

/// <summary>
/// Represents a phrase element.
/// </summary>
[Serializable]
public class Phrase
{
    /// <summary>
    /// Phrase title.
    /// </summary>
    [XmlAttribute("title")]
    public string Title { get; set; } = "";

    /// <summary>
    /// AddOn identifier who is related with this phrase.
    /// </summary>
    [XmlAttribute("addon_id")]
    public string AddOnId { get; set; } = "";

    /// <summary>
    /// AddOn version string where this phrase was last modified.
    /// </summary>
    [XmlAttribute("version_string")]
    public string VersionString { get; set; } = "";

    /// <summary>
    /// AddOn version identifier where this phrase was last modified.
    /// Identifier of the AddOn version where this phrase was last modified.
    /// </summary>
    [XmlAttribute("version_id")]
    public uint VersionId { get; set; } = 1000010;

    /// <summary>
    /// Phrase text. 
    /// </summary>
    [XmlIgnore]
    public string Content { get; set; } = "";

    /// <summary>
    /// Element only for serializer: used for converting CDATA to phrase content and vice versa.
    /// </summary>
    [XmlText]
    public XmlNode[] ContentCData
    {
        get => new XmlNode[] { new XmlDocument().CreateCDataSection(Content) };

        set
        {
            if (value == null || value.Length < 1)
            {
                Content = "";
                return;
            }

            Content = value[0].Value ?? "";
        }
    }
}
