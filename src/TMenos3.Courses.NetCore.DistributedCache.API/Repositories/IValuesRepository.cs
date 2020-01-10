using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMenos3.Courses.NetCore.DistributedCache.API.Repositories
{
    public interface IValuesRepository
    {
        Task<IEnumerable<string>> GetAllAsync();

        Task AddAsync(string value);
    }
}
