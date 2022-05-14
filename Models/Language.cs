// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using System.ComponentModel;
using System.Xml.Serialization;

namespace HLmod.XenForo.LanguageTool.Models;

/// <summary>
/// Represents a language with phrases.
/// </summary>
[Serializable]
[XmlRoot("language")]
public class Language
{
    /// <summary>
    /// Language title (like "Russian (RU)").
    /// </summary>
    [XmlAttribute("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Format for displaying dates.
    /// </summary>
    [XmlAttribute("date_format")]
    public string? DateFormat { get; set; }

    /// <summary>
    /// Format for displaying timestamps.
    /// </summary>
    [XmlAttribute("time_format")]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// Format for displaying currencies (prices).
    /// </summary>
    [XmlAttribute("currency_format")]
    public string? CurrencyFormat { get; set; }

    /// <summary>
    /// Day on which the week starts.
    /// </summary>
    [XmlAttribute("week_start")]
    public int WeekStart { get; set; } = 1;

    /// <summary>
    /// The character set that separates the fractional part from the integer in numbers.
    /// </summary>
    [XmlAttribute("decimal_point")]
    public string? DecimalPoint { get; set; }

    /// <summary>
    /// The character set that separates thousandths in numbers.
    /// </summary>
    [XmlAttribute("thousands_separator")]
    public string? ThousandsSeparator { get; set; }

    /// <summary>
    /// The character that separates labels from text.
    /// </summary>
    [XmlAttribute("label_separator")]
    public string? LabelSeparator { get; set; }

    /// <summary>
    /// Comma character.
    /// </summary>
    [XmlAttribute("comma_separator")]
    public string? CommaSeparator { get; set; }

    /// <summary>
    /// Ellipsis characters.
    /// </summary>
    [XmlAttribute("ellipsis")]
    public string? Ellipsis { get; set; }

    /// <summary>
    /// Parenthesis open symbol.
    /// </summary>
    [XmlAttribute("parenthesis_open")]
    public string? ParenthesisOpen { get; set; }

    /// <summary>
    /// Parenthesis close symbol.
    /// </summary>
    [XmlAttribute("parenthesis_close")]
    public string? ParenthesisClose { get; set; }

    /// <summary>
    /// Language code ("ru-RU").
    /// </summary>
    [XmlAttribute("language_code")]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Text direction (LTR, RTL).
    /// </summary>
    [XmlAttribute("text_direction")]
    public string? TextDirection { get; set; }

    /// <summary>
    /// This language can be selected by user. Introduced in XenForo 2.2.
    /// </summary>
    [XmlAttribute("user_selectable")]
    public uint UserSelectable { get; set; } = 1;

    /// <summary>
    /// Version where language export performed.
    /// </summary>
    [XmlAttribute("base_version_id")]
    public uint BaseVersionId { get; set; } = 1_00_00_1_0;

    /// <summary>
    /// Export version (XF major version).
    /// </summary>
    [XmlAttribute("export_version")]
    public uint ExportVersion { get; set; } = 2;

    /// <summary>
    /// Export addon id (if present).
    /// </summary>
    public string? AddonId { get; set; }

    /// <summary>
    /// All existing phrases in this language.
    /// </summary>
    [XmlElement("phrase")]
    public List<Phrase> Phrases { get; set; } = new();
}
