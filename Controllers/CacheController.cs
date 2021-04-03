using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisCacheAspNet.Models;
using RedisCacheAspNet.Services;

namespace RedisCacheAspNet.Controllers
{
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? NotFound() : Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> SetCacheValue([FromBody] NewCacheEntryRequest request)
        {
            await _cacheService.SetCacheValueAsync(request.Key, request.Value);
            return Ok();
        }
    }
}