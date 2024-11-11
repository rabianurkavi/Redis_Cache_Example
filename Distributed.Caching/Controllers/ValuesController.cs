using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpGet("setName")]
        public async Task<IActionResult> Set(string name, string surName)
        {
            await _distributedCache.SetStringAsync("name", name);
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surName));
            return Ok();
        }
        [HttpGet("getName")]
        public async Task<IActionResult> Get()
        {
            var name = await _distributedCache.GetStringAsync("name");
            var surNameBinary = await _distributedCache.GetAsync("surname");
            var surName = Encoding.UTF8.GetString(surNameBinary);
            return Ok(new
            {
                name,
                surName
            });
        }
    }
}