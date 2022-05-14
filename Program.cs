// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using HLmod.XenForo.LanguageTool.Services;
using CommandLine;
using HLmod.XenForo.LanguageTool;
using HLmod.XenForo.LanguageTool.Extensions;

var parameters = Parser.Default.ParseArguments<CommandLineOptions>(args).CastToValue();
foreach (var path in new[] {parameters.Source, parameters.Target})
{
    if (string.IsNullOrWhiteSpace(path))
    {
        return -1;
    }
}

var returnCode = 0;
try
{
    await new Converter(parameters).RunAsync();
}
catch
{
    returnCode = -1;
}

return returnCode;
