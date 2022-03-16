using ExemploRedis.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ExemploRedis.Services
{
    public class PokemonCacheService : ICacheService<Pokemon>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _options;
        private const string Prefix = "pokemon_";

        public PokemonCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
                SlidingExpiration = TimeSpan.FromSeconds(60)
            };
        }

        public async Task<Pokemon> Get(int id)
        {
            var key = Prefix + id;
            var cache = await _distributedCache.GetStringAsync(key);
            if (cache is null)
            {
                return null;
            }
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(cache);
            return pokemon;
        }

        public async Task Set(Pokemon content)
        {
            var key = Prefix + content.Id;
            var pokemonString = JsonConvert.SerializeObject(content);
            await _distributedCache.SetStringAsync(key, pokemonString, _options);
        }
    }
}
