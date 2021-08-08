using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.FunTranslationsClient
{
    /// <inheritdoc cref="IFunTranslationsService" />
    public class FunTranslationsService : IFunTranslationsService
    {
        private readonly HttpClient _client;
        private readonly ILogger<FunTranslationsService> _logger;
        private readonly JsonSerializerOptions _serialiserOptions;

        /// <summary>
        /// Creates a <see cref="FunTranslationsService"/>.
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public FunTranslationsService(
            HttpClient client,
            ILogger<FunTranslationsService> logger)
        {
            _client = client;
            _logger = logger;

            client.BaseAddress = new Uri("https://api.funtranslations.com/translate/");

            _serialiserOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <inheritdoc />
        public async Task<string> TranslateToYoda(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var response = await GetTranslation("yoda", text);

            if (response.IsSuccessStatusCode)
            {
                return await TryGetTranslation(response);
            }

            _logger.LogError(
                "Failed translate to Yoda from FunTranslations HTTP endpoint. Status code: {statusCode}, error content: {error}",
                response.StatusCode,
                response.Content?.ReadAsStringAsync());

            throw new Exception($"Failed to translate '{text}' using FunTranslations");
        }

        /// <inheritdoc />
        public async Task<string> TranslateToShakespeare(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var response = await GetTranslation("shakespeare", text);

            if (response.IsSuccessStatusCode)
            {
                return await TryGetTranslation(response);
            }

            _logger.LogError(
                "Failed translate to Shakespeare from FunTranslations HTTP endpoint. Status code: {statusCode}, error content: {error}",
                response.StatusCode,
                response.Content?.ReadAsStringAsync());

            throw new Exception($"Failed to translate '{text}' using FunTranslations");
        }

        private async Task<HttpResponseMessage> GetTranslation(string type, string text)
        {
            var formContent = new FormUrlEncodedContent(new[]
                        {
                new KeyValuePair<string, string>("text", text)
            });

            // TODO: Handle rate limiting, retry etc before failing. Also prefer using a failed result type instead of throwing. Applies generally.

            var response = await _client.PostAsync($"{type}.json", formContent);
            return response;
        }

        private async Task<string> TryGetTranslation(HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            var translation = await JsonSerializer.DeserializeAsync<Translation>(contentStream, _serialiserOptions);

            return translation?.Contents?.Translated;
        }
    }
}
