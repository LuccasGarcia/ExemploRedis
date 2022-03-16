using System.Threading.Tasks;

namespace ExemploRedis.Services.Interfaces
{
    public interface ICacheService<T>
    {
        Task<T> Get(int id);
        Task Set(T content);
    }
}
