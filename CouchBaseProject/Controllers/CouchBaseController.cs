using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using CouchBaseProject.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace CouchBaseProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouchBaseController : ControllerBase
{
    private readonly IBucketProvider _bucketProvider;
    private readonly DbContext _context;

    public CouchBaseController(IBucketProvider bucketProvider, DbContext context)
    {
        _bucketProvider = bucketProvider;
        _context = context;
    }

    [HttpGet("Get")]
    public async Task<ErrorLog> Get(string id)
    {
        IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
        ICouchbaseCollection collection = bucket.DefaultCollection();

        IGetResult temp = await collection.GetAsync(id);

        return temp.ContentAs<ErrorLog>()!;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(ErrorLog errlog)
    {
        IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
        ICouchbaseCollection collection = bucket.DefaultCollection();

        IMutationResult result = await collection.InsertAsync(errlog.Id.ToString(), errlog);
        if (result.MutationToken != null)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(ErrorLog errlog)
    {

        IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
        ICouchbaseCollection collection = bucket.DefaultCollection();

        IMutationResult result = await collection.UpsertAsync(errlog.Id.ToString(), errlog);
        if (result.MutationToken != null)
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
    public async Task<IActionResult> SetCache(int count)
    {

        List<ErrorLog> sqlData = _context.Errlogs.ToList().GetRange(0, count);
        IBucket bucket = await _bucketProvider.GetBucketAsync("Errors");
        ICouchbaseCollection collection = bucket.DefaultCollection();

        foreach (var item in sqlData)
        {
            await collection.InsertAsync(item.Id.ToString(), item);
        }

        return Ok();
    }

}
