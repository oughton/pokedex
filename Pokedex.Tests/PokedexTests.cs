using NSubstitute;
using Pokedex.PokeApiClient;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Tests
{
    public class PokedexTests
    {
        private readonly IPokeApiService _pokeApiService;
        private readonly Pokedex _pokedex;

        public PokedexTests()
        {
            _pokeApiService = Substitute.For<IPokeApiService>();

            _pokedex = new Pokedex(_pokeApiService);
        }

        [Fact]
        public async Task GetPokemonSpeciesSuccessfully()
        {
            var species = new PokemonSpeciesExpanded
            {
                Habitat = new PokemonHabitat
                {
                    Name = "rare"
                },
                IsLegendary = true,
                FlavorTextEntries = new[]
                {
                    new FlavorText
                    {
                        Text = "test-fr",
                        Language = new Language
                        {
                            Name = "fr"
                        }
                    },
                    new FlavorText
                    {
                        Text = "test-en",
                        Language = new Language
                        {
                            Name = "en"
                        }
                    }
                }
            };

            _pokeApiService.GetPokemonSpecies("mewtwo")
                .Returns(species);

            var mewtwo = await _pokedex.GetPokemon("mewtwo");

            Assert.NotNull(mewtwo);
            Assert.Equal("mewtwo", mewtwo.Name);
            Assert.Equal("rare", mewtwo.Habitat);
            Assert.True(mewtwo.IsLegendary);
            Assert.Equal("test-en", mewtwo.Description);
        }

        [Fact]
        public async Task GetPokemonSpeciesWithMissingHabitat()
        {
            var species = new PokemonSpeciesExpanded
            {
                IsLegendary = true,
                FlavorTextEntries = new[]
                {
                    new FlavorText
                    {
                        Text = "test",
                        Language = new Language
                        {
                            Name = "en"
                        }
                    }
                }
            };

            _pokeApiService.GetPokemonSpecies("mewtwo")
                .Returns(species);

            var mewtwo = await _pokedex.GetPokemon("mewtwo");

            Assert.NotNull(mewtwo);
            Assert.Equal("mewtwo", mewtwo.Name);
            Assert.Equal("unknown", mewtwo.Habitat);
            Assert.True(mewtwo.IsLegendary);
            Assert.Equal("test", mewtwo.Description);
        }

        [Fact]
        public async Task GetPokemonSpeciesWithMissingDescription()
        {
            var species = new PokemonSpeciesExpanded
            {
                IsLegendary = false
            };

            _pokeApiService.GetPokemonSpecies("mewtwo")
                .Returns(species);

            var mewtwo = await _pokedex.GetPokemon("mewtwo");

            Assert.NotNull(mewtwo);
            Assert.Equal("mewtwo", mewtwo.Name);
            Assert.Equal("unknown", mewtwo.Habitat);
            Assert.False(mewtwo.IsLegendary);
            Assert.Equal("A cool pokemon!", mewtwo.Description);
        }

        [Fact]
        public async Task GetPokemonSpeciesStripsSpecialEscapeCharacters()
        {
            var species = new PokemonSpeciesExpanded
            {
                Habitat = new PokemonHabitat
                {
                    Name = "rare"
                },
                IsLegendary = true,
                FlavorTextEntries = new[]
                {
                    new FlavorText
                    {
                        Text = "\ntest-en\nfoo\fbar",
                        Language = new Language
                        {
                            Name = "en"
                        }
                    }
                }
            };

            _pokeApiService.GetPokemonSpecies("mewtwo")
                .Returns(species);

            var mewtwo = await _pokedex.GetPokemon("mewtwo");

            Assert.NotNull(mewtwo);
            Assert.Equal("mewtwo", mewtwo.Name);
            Assert.Equal("rare", mewtwo.Habitat);
            Assert.True(mewtwo.IsLegendary);
            Assert.Equal(" test-en foo bar", mewtwo.Description);
        }

        [Fact]
        public async Task GetPokemonSpeciesNotFound()
        {
            var mewtwo = await _pokedex.GetPokemon("mewtwo");

            Assert.Null(mewtwo);
        }
    }
}
