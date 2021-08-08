using Microsoft.Extensions.Logging;
using Pokedex.FunTranslationsClient;
using System;
using System.Threading.Tasks;

namespace Pokedex
{
    /// <inheritdoc />
    public class PokedexTranslator : IPokedexTranslator
    {
        private readonly IFunTranslationsService _translatorService;
        private readonly ILogger<PokedexTranslator> _logger;

        /// <summary>
        /// Creates a <see cref="PokedexTranslator"/>.
        /// </summary>
        /// <param name="translatorService">The translator service.</param>
        /// <param name="logger">The logger.</param>
        public PokedexTranslator(
            IFunTranslationsService translatorService,
            ILogger<PokedexTranslator> logger)
        {
            _logger = logger;
            _translatorService = translatorService;
        }

        /// <inheritdoc />
        public async Task<Pokemon> Translate(Pokemon pokemon)
        {
            if (pokemon == null)
                throw new ArgumentNullException(nameof(pokemon));

            var description = await TranslateDescription(pokemon);

            return new Pokemon(pokemon.Name, description, pokemon.Habitat, pokemon.IsLegendary);
        }

        private async Task<string> TranslateDescription(Pokemon pokemon)
        {
            if (pokemon.Habitat.Equals("cave", StringComparison.OrdinalIgnoreCase) || pokemon.IsLegendary)
            {
                return await Translate(_translatorService.TranslateToYoda, pokemon.Description);
            }

            return await Translate(_translatorService.TranslateToShakespeare, pokemon.Description);
        }

        private async Task<string> Translate(Func<string, Task<string>> translator, string text)
        {
            try
            {
                return await translator(text) ?? text;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to translate Pokemon description: {text}", text);
                return text;
            }
        }
    }
}
