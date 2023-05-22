using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using CouchBaseProject.Models;
using Microsoft.AspNetCore.Mvc;


namespace CouchBaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouchBaseController : ControllerBase
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly CalismaVeriTabaniContext _context;

        public CouchBaseController(IBucketProvider bucketProvider, CalismaVeriTabaniContext context)
        {
            _bucketProvider = bucketProvider;
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<Errlog> Get(string id)
        {
            IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
            ICouchbaseCollection collection = bucket.DefaultCollection();

            IGetResult temp = await collection.GetAsync(id);

            return temp.ContentAs<Errlog>()!;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Errlog errlog)
        {
            IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
            ICouchbaseCollection collection = bucket.DefaultCollection();

            IMutationResult result = await collection.InsertAsync(errlog.Guid.ToString(), errlog);
            if (result.MutationToken != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Errlog errlog)
        {

            IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
            ICouchbaseCollection collection = bucket.DefaultCollection();

            IMutationResult result =  await collection.UpsertAsync(errlog.Guid.ToString(), errlog);
            if(result.MutationToken != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
            ICouchbaseCollection collection = bucket.DefaultCollection();

            await collection.RemoveAsync(id);

            return Ok();
        }


        [HttpGet("SetCache")]
        public async Task<IActionResult> SetCache()
        {

            List<Errlog> sqlData = _context.Errlogs.ToList();
            IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
            ICouchbaseCollection collection = bucket.DefaultCollection();

            foreach (var item in sqlData)
            {
                await collection.InsertAsync(item.Guid.ToString(), item);
            }

            return Ok();
        }




    }

}
