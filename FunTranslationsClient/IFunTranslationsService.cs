using System.Threading.Tasks;

namespace Pokedex.FunTranslationsClient
{
    /// <summary>
    /// A Fun Translations API service.
    /// </summary>
    public interface IFunTranslationsService
    {
        /// <summary>
        /// Translates text to Shakespeare.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <returns>The translated text; otherwise null.</returns>
        Task<string> TranslateToShakespeare(string text);

        /// <summary>
        /// Translates text to Yoda.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <returns>The translated text; otherwise null.</returns>
        Task<string> TranslateToYoda(string text);
    }
}
