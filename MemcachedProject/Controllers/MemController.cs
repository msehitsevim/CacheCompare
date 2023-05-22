using MemcachedProject.Models;
using MemcachedProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MemcachedProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemController : ControllerBase
{
    private readonly IErrorRepository _repository;

    public MemController(IErrorRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetById")]
    public async Task<string> GetById(int id)
    {
       return await _repository.GetErrorByIdAsync(id);
    }
   
    [HttpPost("Create")]
    public async Task<string> Create(Errlog errlog)
    {
       return await _repository.AddErrorAsync(errlog);
    }

    [HttpPut("Update")]
    public async Task<string> Update(Errlog errlog)
    {
       return await _repository.UpdateErrorAsync(errlog);
    }

    [HttpDelete("Delete")]
    public async Task<string> Delete(int id)
    {
        string existingData = await _repository.GetErrorByIdAsync(id);

        if (existingData == "Değer Bulunamadı.")
        {
            return "Değer Bulunamadı.";
        }

        string result = await _repository.DeleteErrorAsync(id);

        return result;
    }

    [HttpGet("SetCache")]
    public async Task<string> SetCache()
    {
        return await _repository.GetAllErrorAsync();
    }

}
