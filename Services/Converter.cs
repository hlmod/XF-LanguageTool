using HLmod.XenForo.LanguageTool;
using HLmod.XenForo.LanguageTool.LanguageProviders;

namespace HLmod.XenForo.LanguageTool.Services
{
    internal class Converter
    {
        private readonly CommandLineOptions _options;

        public Converter(CommandLineOptions options)
        {
            _options = options;
        }

        public async Task RunAsync()
        {
            var sourceProvider = ResolveLanguageProvider(_options.Source);
            var targetProvider = ResolveLanguageProvider(_options.Target);

            var language = await sourceProvider.LoadAsync(_options.Source);
            if (language == null)
            {
                return;
            }

            await targetProvider.SaveAsync(_options.Target, language);
        }

        private ILanguageProvider ResolveLanguageProvider(string filePath)
        {
            return new FileInfo(filePath).Extension switch
            {
                ".xml" => new XFProvider(),
                ".po" => new PoProvider(),

                _ => throw new Exception($"Unsupported file format")
            };
        }
    }
}
