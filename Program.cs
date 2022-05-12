// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using HLmod.XenForo.LanguageTool.Services;
using CommandLine;
using HLmod.XenForo.LanguageTool;
using HLmod.XenForo.LanguageTool.Extensions;

var parameters = Parser.Default.ParseArguments<CommandLineOptions>(args).CastToValue();
await new Converter(parameters).RunAsync();
