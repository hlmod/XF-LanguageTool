// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using System.Xml.Serialization;

namespace HLmod.XenForo.LanguageTool.Infrastructure;

/// <summary>
/// A wrapper for serializing strings in CData element.
/// </summary>
/// <see cref="https://stackoverflow.com/a/19832309"/>
public class CDataText : IXmlSerializable
{
    private string _value;

    /// <summary>
    /// Allow direct assignment from string:
    /// CData cdata = "abc";
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator CDataText(string value)
    {
        return new CDataText(value);
    }

    /// <summary>
    /// Allow direct assigment to string
    /// string str = cdata;
    /// </summary>
    /// <param name="cdata"></param>
    /// <returns></returns>
    public static implicit operator string(CDataText cdata)
    {
        return cdata._value;
    }

    public CDataText() : this(string.Empty)
    {
    }

    public CDataText(string value)
    {
        _value = value;
    }

    public override string ToString()
    {
        return _value;
    }

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    public System.Xml.Schema.XmlSchema GetSchema() => null;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

    public void ReadXml(System.Xml.XmlReader reader)
    {
        _value = reader.ReadElementString();
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
        writer.WriteCData(_value);
    }
}
