﻿using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using RedisProject.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.Serialization.Formatters.Binary;
using Entities;

namespace MemcachedProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RedisController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private readonly DbContext _context;

    public RedisController(DbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        byte[] temp =  await _cache.GetAsync("Error_"+id.ToString());
        
        return Ok(ByteArrayToObject(temp!));
    }

    [HttpPost("Create")]
    public async Task<ErrorLog> Create(ErrorLog errlog)
    {
        await _cache.SetAsync($"Error_{errlog.Id}", ObjectToByteArray(errlog));
        
        return errlog;
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(ErrorLog errlog)
    {
        await _cache.SetAsync($"Error_{errlog.Id}", ObjectToByteArray(errlog));

        return Accepted();
    }

    public static byte[] ObjectToByteArray(Object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    public static Object ByteArrayToObject(byte[] arrBytes)
    {
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);
            return obj;
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cache.RemoveAsync($"Error_{id}");
        return NoContent();
    }

    [HttpGet("SetCache")]
    public async Task<string> SetCache(int count)
    {
        List<ErrorLog> sqlData = _context.Errlogs.ToList().GetRange(0,count);

        foreach (var item in sqlData)
        {
           await _cache.SetAsync($"Error_{item.Id}", ObjectToByteArray(item));
        }

        return Ok("test").ToString()!;
    }
}
