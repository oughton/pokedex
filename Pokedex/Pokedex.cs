using Pokedex.PokeApiClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex
{
    /// <inheritdoc />
    public class Pokedex : IPokedex
    {
        private IPokeApiService _pokeApiService;

        /// <summary>
        /// Creates a <see cref="Pokedex"/>.
        /// </summary>
        /// <param name="pokeApiService">The PokeApi service.</param>
        public Pokedex(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;
        }

        /// <inheritdoc />
        public async Task<Pokemon> GetPokemon(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var species = await _pokeApiService.GetPokemonSpecies(name);

            if (species == null)
            {
                return null;
            }

            return new Pokemon(
                name,
                GetEnPokemonDescription(species),
                species.Habitat?.Name ?? "unknown",
                species.IsLegendary);
        }

        private static string GetEnPokemonDescription(PokemonSpeciesExpanded species)
        {
            string description = null;

            if (species.FlavorTextEntries?.Count > 0)
            {
                description = species.FlavorTextEntries
                    .Where(t => t.Language?.Name == "en")
                    .Select(t => t.Text)
                    .FirstOrDefault();

                if (description != null)
                {
                    description = description.Replace('\n', ' ');
                    description = description.Replace('\f', ' ');
                }
            }

            return description ?? "A cool pokemon!";
        }
    }
}
