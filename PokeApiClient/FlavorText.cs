using System.Text.Json.Serialization;

namespace Pokedex.PokeApiClient
{
    public class FlavorText
    {
        // TODO: Use snake case naming policy instead (not available out of the box in System.Text.Json).
        [JsonPropertyName("flavor_text")]
        public string Text { get; set; }

        public Language Language { get; set; }
    }
}
