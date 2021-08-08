using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.PokeApiClient
{
    public class PokemonSpeciesExpanded
    {
        public PokemonHabitat Habitat { get; set; }

        // TODO: Use snake case naming policy instead (not available out of the box in System.Text.Json).
        [JsonPropertyName("flavor_text_entries")]
        public IList<FlavorText> FlavorTextEntries { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }
    }
}
