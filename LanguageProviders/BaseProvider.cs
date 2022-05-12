// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using HLmod.XenForo.LanguageTool.Models;

namespace HLmod.XenForo.LanguageTool.LanguageProviders;

internal abstract class BaseProvider : ILanguageProvider
{
    public Language? Load(string path)
    {
        return LoadAsync(path)
            .GetAwaiter()
            .GetResult();
    }

    public void Save(string path, Language language)
    {
        SaveAsync(path, language)
            .GetAwaiter()
            .GetResult();
    }

    public abstract Task<Language?> LoadAsync(string path);
    public abstract Task SaveAsync(string path, Language language);
}
