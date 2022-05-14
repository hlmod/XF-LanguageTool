// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using System.Xml.Serialization;
using HLmod.XenForo.LanguageTool.Extensions;
using HLmod.XenForo.LanguageTool.Models;

namespace HLmod.XenForo.LanguageTool.LanguageProviders;

internal class XFProvider : BaseProvider
{
    public readonly XmlSerializer LanguageSerializer =
        new XmlSerializer(typeof(Language));

    public override Task<Language?> LoadAsync(string path)
    {
        using (var file = File.OpenRead(path))
        {
            var language = (Language?) LanguageSerializer.Deserialize(file);
            return new Task<Language?>(() => language).AsStarted();
        }
    }

    public override Task SaveAsync(string path, Language language)
    {
        using (var file = File.OpenWrite(path))
        {
            LanguageSerializer.SerializeWithoutNamespaces(file, language);
        }

        return new Task(() => {}).AsStarted();
    }
}
