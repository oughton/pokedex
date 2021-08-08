using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Pokedex.FunTranslationsClient;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Tests
{
    public class PokedexTranslatorTests
    {
        private readonly IFunTranslationsService _translationsService;
        private readonly PokedexTranslator _translator;

        public PokedexTranslatorTests()
        {
            _translationsService = Substitute.For<IFunTranslationsService>();
            var logger = Substitute.For<ILogger<PokedexTranslator>>();

            _translator = new PokedexTranslator(_translationsService, logger);
        }

        [Fact]
        public async Task TranslateToYodaByCave()
        {
            var pokemon = new Pokemon("mewtwo", "foo bar", "cave", false);

            _translationsService.TranslateToYoda("foo bar")
                .Returns("bat");

            var translated = await _translator.Translate(pokemon);

            Assert.NotNull(translated);
            Assert.Equal("bat", translated.Description);
        }

        [Fact]
        public async Task TranslateToYodaByLegendary()
        {
            var pokemon = new Pokemon("mewtwo", "foo bar", "rare", true);

            _translationsService.TranslateToYoda("foo bar")
                .Returns("bat");

            var translated = await _translator.Translate(pokemon);

            Assert.NotNull(translated);
            Assert.Equal("bat", translated.Description);
        }

        [Fact]
        public async Task TranslateToShakespeare()
        {
            var pokemon = new Pokemon("weedle", "foo bar", "grass", false);

            _translationsService.TranslateToShakespeare("foo bar")
                .Returns("bat");

            var translated = await _translator.Translate(pokemon);

            Assert.NotNull(translated);
            Assert.Equal("bat", translated.Description);
        }

        [Fact]
        public async Task TranslateNullTranslation()
        {
            var pokemon = new Pokemon("weedle", "foo bar", "grass", false);

            _translationsService.TranslateToShakespeare("foo bar")
                .Returns((string)null);

            var translated = await _translator.Translate(pokemon);

            Assert.NotNull(translated);
            Assert.Equal("foo bar", translated.Description);
        }

        [Fact]
        public async Task TranslateFailure()
        {
            var pokemon = new Pokemon("weedle", "foo bar", "grass", false);

            _translationsService.TranslateToShakespeare("foo bar")
                .Throws(new Exception("test"));

            var translated = await _translator.Translate(pokemon);

            Assert.NotNull(translated);
            Assert.Equal("foo bar", translated.Description);
        }
    }
}
