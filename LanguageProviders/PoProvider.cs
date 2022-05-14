// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using HLmod.XenForo.LanguageTool.Extensions;
using HLmod.XenForo.LanguageTool.Models;
using Karambolo.PO;

namespace HLmod.XenForo.LanguageTool.LanguageProviders;

internal class PoProvider : BaseProvider
{
    public override async Task<Language?> LoadAsync(string path)
    {
        var parser = new POParser(new POParserSettings
        {
            PreserveHeadersOrder = true,
            ReadHeaderOnly = false,
            SkipComments = false,
            SkipInfoHeaders = false,
            StringDecodingOptions = new()
            {
                KeepKeyStringsPlatformIndependent = true,
                KeepTranslationStringsPlatformIndependent = true
            }
        });

        using (var file = File.OpenRead(path))
        {
            var result = parser.Parse(file);
            if (result.Catalog != null)
                return await FromPoResult(result.Catalog);
        }

        return null;
    }

    public override async Task SaveAsync(string path, Language language)
    {
        var catalog = new POCatalog()
        {
            Headers = new Dictionary<string, string>()
        };

        catalog.Language = language.LanguageCode;
        catalog.Encoding = "UTF-8";
        catalog.PluralFormCount = 1;

        // Save language metadata.
        // TODO: move to HeaderComments. Weblate, for example, strips all of these content when imports file.
        catalog.Headers["XF-Title"] = language.Title;
        catalog.Headers["XF-DateFormat"] = language.DateFormat;
        catalog.Headers["XF-TimeFormat"] = language.TimeFormat;
        catalog.Headers["XF-CurrencyFormat"] = language.CurrencyFormat;
        catalog.Headers["XF-WeekStart"] = language.WeekStart.ToString();
        catalog.Headers["XF-DecimalPoint"] = language.DecimalPoint;
        catalog.Headers["XF-ThousandsSeparator"] = language.ThousandsSeparator;
        catalog.Headers["XF-LabelSeparator"] = language.LabelSeparator;
        catalog.Headers["XF-CommaSeparator"] = language.CommaSeparator;
        catalog.Headers["XF-Ellipsis"] = language.Ellipsis;
        catalog.Headers["XF-ParenthesisOpen"] = language.ParenthesisOpen;
        catalog.Headers["XF-ParenthesisClose"] = language.ParenthesisClose;
        catalog.Headers["XF-TextDirection"] = language.TextDirection;
        catalog.Headers["XF-UserSelectable"] = language.UserSelectable.ToString();
        catalog.Headers["XF-BaseVersionId"] = language.BaseVersionId.ToString();
        catalog.Headers["XF-ExportVersion"] = language.ExportVersion.ToString();

        // Save phrases metadata.
        foreach (var phrase in language.Phrases)
        {
            var entry = new POSingularEntry(new POKey(phrase.Title))
            {
                Comments = new List<POComment>(),
                Translation = phrase.Content
            };
            entry.Comments.Add(BuildComment(phrase));

            catalog.Add(entry);
        }

        // Write file.
        var generator = new POGenerator(new POGeneratorSettings
        {
            IgnoreEncoding = true,
            IgnoreLineBreaks = true,
            IgnoreLongLines = true,
            PreserveHeadersOrder = true,
            SkipComments = false,
            SkipInfoHeaders = false
        });

        using (var file = File.OpenWrite(path))
        {
            generator.Generate(file, catalog);
            await file.FlushAsync();
        }
    }

    private Task<Language?> FromPoResult(POCatalog catalog)
    {
        // Fill language header.
        var language = new Language();

        // Parameters order here should match with XML parameters in export.
        var headers = catalog.Headers;
        language.Title = headers.GetOrDefault("XF-Title", language.Title);
        if (string.IsNullOrWhiteSpace(language.Title))
        {
            language.Title = "Unknown language"; // This field is required for importing.
        }

        // Formats (date, time, currencies)
        language.DateFormat = headers.GetOrDefault("XF-DateFormat", language.DateFormat);
        language.TimeFormat = headers.GetOrDefault("XF-TimeFormat", language.TimeFormat);
        language.CurrencyFormat = headers.GetOrDefault("XF-CurrencyFormat", language.CurrencyFormat);

        // Week start
        language.WeekStart = int.Parse(headers.GetOrDefault("XF-WeekStart", language.WeekStart.ToString()));

        // Points, separators
        language.DecimalPoint = headers.GetOrDefault("XF-DecimalPoint", language.DecimalPoint);
        language.ThousandsSeparator = headers.GetOrDefault("XF-ThousandsSeparator", language.ThousandsSeparator);
        language.LabelSeparator = headers.GetOrDefault("XF-LabelSeparator", language.LabelSeparator);
        language.CommaSeparator = headers.GetOrDefault("XF-CommaSeparator", language.CommaSeparator);
        language.Ellipsis = headers.GetOrDefault("XF-Ellipsis", language.Ellipsis);

        // Parenthesis.
        language.ParenthesisOpen = headers.GetOrDefault("XF-ParenthesisOpen", language.ParenthesisOpen);
        language.ParenthesisClose = headers.GetOrDefault("XF-ParenthesisClose", language.ParenthesisClose);

        // Basics (language code, etc).
        language.LanguageCode = catalog.Language ?? language.LanguageCode;
        language.TextDirection = headers.GetOrDefault("XF-TextDirection", language.TextDirection);
        language.UserSelectable = uint.Parse(headers.GetOrDefault("XF-UserSelectable", language.UserSelectable.ToString()));
        language.BaseVersionId = uint.Parse(headers.GetOrDefault("XF-BaseVersionId", language.BaseVersionId.ToString()));
        language.ExportVersion = uint.Parse(headers.GetOrDefault("XF-ExportVersion", language.ExportVersion.ToString()));

        // Now setup phrases.
        foreach (POSingularEntry poPhrase in catalog)
        {
            var phrase = new Phrase();
            phrase.Title = poPhrase.Key.Id;
            phrase.Content = poPhrase.Translation;

            // Additional attributes.
            if (!poPhrase.Comments.Any(e => e is POReferenceComment))
            {
                continue;
            }

            var reference = ((POReferenceComment)poPhrase.Comments.Single(e => e is POReferenceComment)).References[0].FilePath.Split(':');
            if (reference.Length < 3)
            {
                continue;
            }
            else if (reference.Length > 3)
            {
                // Looks like version string contains colons.
                reference[2] = string.Join(':', reference.Skip(2));
                reference = reference.Take(3).ToArray();
            }

            phrase.AddOnId = reference[0];
            phrase.VersionId = uint.Parse(reference[1]);
            phrase.VersionString = reference[2];

            language.Phrases.Add(phrase);
        }

        return new Task<Language?>(() => language).AsStarted();
    }
    private POReferenceComment BuildComment(Phrase phrase)
    {
        var comment = new POReferenceComment()
        {
            References = new List<POSourceReference>()
        };

        comment.References.Add(new POSourceReference($"{phrase.AddOnId}:{phrase.VersionId}:{phrase.VersionString}", 0));

        return comment;
    }
}
