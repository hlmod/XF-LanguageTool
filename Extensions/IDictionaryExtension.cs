// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

namespace HLmod.XenForo.LanguageTool.Extensions;

internal static class IDictionaryExtension
{
    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        => GetOrDefault<TKey, TValue>(dict, key, default);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defValue)
    {
        TValue value;
        if (!dict.TryGetValue(key, out value))
        {
            value = defValue;
        }

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        return value;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    }
}

