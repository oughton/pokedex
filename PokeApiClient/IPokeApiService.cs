using System.Threading.Tasks;

namespace Pokedex.PokeApiClient
{
    /// <summary>
    /// A PokeApi API service.
    /// </summary>
    public interface IPokeApiService
    {
        /// <summary>
        /// Gets a Pokemon by name.
        /// </summary>
        /// <param name="name">The Pokemon name.</param>
        /// <returns>The Pokemon if found; otherwise null.</returns>
        Task<Pokemon> GetPokemon(string name);

        /// <summary>
        /// Get a Pokemon species by name.
        /// </summary>
        /// <param name="name">The Pokemon name.</param>
        /// <returns>The Pokemon's species if found; otherwise null.</returns>
        Task<PokemonSpeciesExpanded> GetPokemonSpecies(string name);
    }
}
