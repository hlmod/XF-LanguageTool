// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

using CommandLine;

namespace HLmod.XenForo.LanguageTool;

[Verb("xml")]
public class CommandLineOptions
{
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages",
        Default = false)]
    public bool Verbose { get; set; } = false;

    [Value(0, HelpText = "Path to .xml/.po file for converting",
        Default = "")]
    public string Source { get; set; } = "";

    [Value(1, HelpText = "Path to .po/.xml file output",
        Default = "")]
    public string Target { get; set; } = "";
}
