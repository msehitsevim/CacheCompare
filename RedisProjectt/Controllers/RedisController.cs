using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;


namespace RedisProjectt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDatabase _database;

        public RedisController(IDatabase database)
        {
            _database = database;
        }

        // GET: api/<RedisController>
        [HttpGet]
        public string Get([FromQuery]string key)
        {
            return _database.StringGet(key)!;
        }


        // POST api/<RedisController>
        [HttpPost]
        public void Post([FromBody] KeyValuePair<string,string> keyValue)
        {
            _database.StringSet(keyValue.Key, keyValue.Value);
        }

    }
}
