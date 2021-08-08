using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokedex _pokedex;
        private readonly IPokedexTranslator _translator;

        public PokemonController(
            IPokedex pokedex,
            IPokedexTranslator translator)
        {
            _pokedex = pokedex;
            _translator = translator;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            var pokemon = await _pokedex.GetPokemon(name);

            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }

        [HttpGet("translated/{name}")]
        public async Task<ActionResult> GetTranslated(string name)
        {
            var pokemon = await _pokedex.GetPokemon(name);

            if (pokemon == null)
            {
                return NotFound();
            }

            pokemon = await _translator.Translate(pokemon);

            return Ok(pokemon);
        }
    }
}
