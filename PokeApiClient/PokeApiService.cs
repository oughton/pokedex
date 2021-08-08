using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.PokeApiClient
{
    /// <inheritdoc cref="IPokeApiService" />
    public class PokeApiService : IPokeApiService
    {
        private readonly HttpClient _client;
        private readonly ILogger<PokeApiService> _logger;
        private readonly JsonSerializerOptions _serialiserOptions;

        /// <summary>
        /// Creates a <see cref="PokeApiService"/>.
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public PokeApiService(
            HttpClient client,
            ILogger<PokeApiService> logger)
        {
            _client = client;
            _logger = logger;

            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

            _serialiserOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <inheritdoc />
        public async Task<Pokemon> GetPokemon(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);

            var response = await _client.GetAsync($"pokemon/{name}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Pokemon>(contentStream, _serialiserOptions);
            }

            // TODO: Handle rate limiting, retry etc before failing. Also prefer using a failed result type instead of throwing. Applies generally.

            _logger.LogError(
                "Failed to get Pokemon from PokeApi HTTP endpoint. Status code: {statusCode}, error content: {error}",
                response.StatusCode,
                response.Content?.ReadAsStringAsync());

            throw new Exception($"Failed to get Pokemon '{name}' from PokeApi");
        }

        /// <inheritdoc />
        public async Task<PokemonSpeciesExpanded> GetPokemonSpecies(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);

            var pokemon = await GetPokemon(name);

            if (pokemon == null)
            {
                return null;
            }

            var response = await _client.GetAsync(pokemon.Species.Url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<PokemonSpeciesExpanded>(contentStream, _serialiserOptions);
            }

            _logger.LogError(
                "Failed to get Pokemon species from PokeApi HTTP endpoint. Status code: {statusCode}, error content: {error}",
                response.StatusCode,
                response.Content?.ReadAsStringAsync());

            throw new Exception($"Failed to get Pokemon '{name}' species from PokeApi");
        }
    }
}
