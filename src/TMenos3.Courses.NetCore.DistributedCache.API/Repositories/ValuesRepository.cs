using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMenos3.Courses.NetCore.DistributedCache.API.Repositories
{
    public class ValuesRepository : IValuesRepository
    {
        private static List<string> _values = new List<string>();

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            await Task.Delay(1000);

            return _values;
        }

        public Task AddAsync(string value)
        {
            _values.Add(value);
            return Task.CompletedTask;
        }
    }
}
