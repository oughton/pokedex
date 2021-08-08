using System.Threading.Tasks;

namespace Pokedex
{
    /// <summary>
    /// A handy translator for your everyday Pokedex translating needs.
    /// </summary>
    public interface IPokedexTranslator
    {
        /// <summary>
        /// Translates a Pokemon from english into something fun.
        /// </summary>
        /// <param name="pokemon">The Pokemon to translate.</param>
        /// <returns>The translated Pokemon.</returns>
        Task<Pokemon> Translate(Pokemon pokemon);
    }
}
