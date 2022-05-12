// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using CommandLine;

namespace HLmod.XenForo.LanguageTool.Extensions;

internal static class CommandLineParserResultExtension
{
    public static TResult CastToValue<TResult>(this ParserResult<TResult> result)
        where TResult : class, new()
    {
        var parameters = (result as Parsed<TResult>)?.Value;
        if (parameters == null)
        {
            parameters = new TResult();
        }

        return parameters;
    }
}
