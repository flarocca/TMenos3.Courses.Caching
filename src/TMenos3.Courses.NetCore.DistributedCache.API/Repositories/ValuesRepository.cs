using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMenos3.Courses.NetCore.DistributedCache.API.Repositories
{
    public class ValuesRepository : IValuesRepository
    {
        public async Task<IEnumerable<string>> GetAllAsync()
        {
            await Task.Delay(1000);

            return new List<string>
            {
                "Value 1",
                "Value 2"
            };
        }
    }
}
