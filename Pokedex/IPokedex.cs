using System.Threading.Tasks;

namespace Pokedex
{
    /// <summary>
    /// A device full of information about Pokemon.
    /// </summary>
    public interface IPokedex
    {
        /// <summary>
        /// Gets a Pokemon.
        /// </summary>
        /// <param name="name">The name of the Pokemon.</param>
        /// <returns>The Pokemon if found; otherwise null.</returns>
        Task<Pokemon> GetPokemon(string name);
    }
}
