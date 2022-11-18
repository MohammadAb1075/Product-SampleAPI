using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public interface IRepository
    {
        Task CommitAsync();
        Task CreateAsync<T>(T model) where T : class;
        Task UpdateAsync<T>(T model) where T : class;
        Task DeleteAsync<T>(string id) where T : class;
        Task<T> GetByIdAsync<T>(string id) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
    }
}
