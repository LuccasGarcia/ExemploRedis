using System.Threading.Tasks;

namespace ExemploRedis.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemon(int id);
    }
}
