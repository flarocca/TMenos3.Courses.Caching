using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMenos3.Courses.NetCore.DistributedCache.API.Extensions;

namespace TMenos3.Courses.NetCore.DistributedCache.API.Repositories
{
    public class CachedValuesRepository : IValuesRepository
    {
        private const string VALUES_KEY = "values";

        private readonly ValuesRepository _repository;
        private readonly IDistributedCache _cache;

        public CachedValuesRepository(ValuesRepository repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public Task<IEnumerable<string>> GetAllAsync() =>
            _cache.GetOrCreateAsync(VALUES_KEY, _repository.GetAllAsync);

        public async Task AddAsync(string value)
        {
            await _repository.AddAsync(value);
            await _cache.RemoveAsync(VALUES_KEY);
        }
    }
}
