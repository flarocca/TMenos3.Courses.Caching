using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using TMenos3.Courses.NetCore.DistributedCache.API.Repositories;

namespace TMenos3.Courses.NetCore.DistributedCache.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const string VALUES_KEY = "values";

        private readonly IValuesRepository _valuesRepository;
        private readonly IDatabase _cache;

        public ValuesController(IValuesRepository valuesRepository, IDatabase cache)
        {
            _valuesRepository = valuesRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllAsync()
        {
            var resultFromCache = await _cache.StringGetAsync(VALUES_KEY);
            if (resultFromCache.IsNull)
            {
                var values = await _valuesRepository.GetAllAsync();
                await _cache.StringSetAsync(VALUES_KEY, JsonConvert.SerializeObject(values));
                return Ok(values);
            }

            return Ok(JsonConvert.DeserializeObject<IEnumerable<string>>(resultFromCache));
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]string value)
        {
            await _valuesRepository.AddAsync(value);
            await _cache.KeyDeleteAsync(VALUES_KEY);

            return NoContent();
        }
    }
}