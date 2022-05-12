// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using HLmod.XenForo.LanguageTool.Models;

namespace HLmod.XenForo.LanguageTool.LanguageProviders;

internal interface ILanguageProvider
{
    /// <summary>
    /// Performs a loading Language from file into <see cref="Language"/>.
    /// </summary>
    /// <param name="path">Path to file</param>
    /// <returns><see cref="Language"/>, if file is successfully deserialized, null otherwise.</returns>
    Language? Load(string path);

    /// <summary>
    /// Performs a asynchoronously loading Language from file into <see cref="Language"/>.
    /// </summary>
    /// <param name="path">Path to file</param>
    /// <returns><see cref="Language"/>, if file is successfully deserialized, null otherwise.</returns>
    Task<Language?> LoadAsync(string path);

    /// <summary>
    /// Performs a saving <see cref="Language"/> to file.
    /// </summary>
    /// <param name="path">Path to file</param>
    /// <param name="language"></param>
    void Save(string path, Language language);

    /// <summary>
    /// Performs a asynchoronously saving <see cref="Language"/> to file.
    /// </summary>
    /// <param name="path">Path to file</param>
    /// <param name="language"></param>
    Task SaveAsync(string path, Language language);
}
