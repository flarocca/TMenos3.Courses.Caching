using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TMenos3.Courses.NetCore.DistributedCache.API.Repositories;

namespace TMenos3.Courses.NetCore.DistributedCache.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const string VALUES_KEY = "values";

        private readonly IValuesRepository _valuesRepository;
        private readonly IDistributedCache _cache;

        public ValuesController(IValuesRepository valuesRepository, IDistributedCache cache)
        {
            _valuesRepository = valuesRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllAsync()
        {
            var resultFromCache = await _cache.GetStringAsync(VALUES_KEY);
            if (string.IsNullOrWhiteSpace(resultFromCache))
            {
                var values = await _valuesRepository.GetAllAsync();
                await _cache.SetStringAsync(VALUES_KEY, JsonConvert.SerializeObject(values));
                return Ok(values);
            }

            return Ok(JsonConvert.DeserializeObject<IEnumerable<string>>(resultFromCache));
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]string value)
        {
            await _valuesRepository.AddAsync(value);
            await _cache.RemoveAsync(VALUES_KEY);

            return NoContent();
        }
    }
}