using ExemploRedis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExemploRedis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ICacheService<Pokemon> _pokemonCacheService;

        public PokemonController(IPokemonService pokemonService, ICacheService<Pokemon> pokemonCacheService)
        {
            _pokemonService = pokemonService;
            _pokemonCacheService = pokemonCacheService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            while (id < 150)
            {
                Pokemon pokemon = await _pokemonCacheService.Get(id);
                if (pokemon is null)
                {
                    pokemon = await _pokemonService.GetPokemon(id);
                    await _pokemonCacheService.Set(pokemon);
                }
                //return Ok(pokemon);
                id++;
            }
            return null;
        }


    }
}
