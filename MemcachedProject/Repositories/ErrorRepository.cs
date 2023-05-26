using Entities;
using Enyim.Caching;
using MemcachedProject.Models;

namespace MemcachedProject.Repositories;

public class ErrorRepository : IErrorRepository
{
    private readonly IMemcachedClient _cache;
    private readonly DbContext _context;

    public ErrorRepository(IMemcachedClient cache, DbContext context)
    {
        _cache = cache;
        _context = context;
    }

    public async Task<ErrorLog> GetErrorByIdAsync(int id)
    {
        var cacheKey = $"Error_{id}";

        var result = await _cache.GetAsync<ErrorLog>(cacheKey);

        if (!result.Success)
        {
            return new ErrorLog { ErrorLine = 0, ErrorMessage = "Aranılan Veri Bulunamadı."};
        }

        return result.Value;
    }

    public async Task<string> AddErrorAsync(ErrorLog err)
    { 
        bool retval = await _cache.SetAsync($"Error_{err.Id}", err, TimeSpan.FromMinutes(5));
        if (!retval)
        {
            return "Belleğe yazılamadı";
        }
        return "İşlem başarılı";
    }

    public async Task<string> UpdateErrorAsync(ErrorLog errlog)
    {
       bool retval = await _cache.ReplaceAsync($"Error_{errlog.Id}", errlog, TimeSpan.FromMinutes(5));
        if (!retval)
        {
            return "Bellekteki veri güncellenemedi";
        }
        return "İşlem başarılı";
    }

    public async Task<string> DeleteErrorAsync(int id)
    {
        await _cache.RemoveAsync($"Error_{id}");

        var getCacheData = await _cache.GetAsync<ErrorLog>($"Error_{id}");

        if (getCacheData.Success)
        {
            return "Bellekte Veri Duruyor. Tekrar deneyin.";
        }

        return "İşlem başarılı.";
    }

    public async Task<string> GetAllErrorAsync(int count)
    {

        List<ErrorLog> sqlData = _context.Errlogs.ToList().GetRange(0,count);
        List<ErrorLog> resultList = new List<ErrorLog>();

        foreach (var item in sqlData)
        {
            await _cache.SetAsync($"Error_{item.Id}", item, TimeSpan.FromMinutes(5));

        }
       return  "bitti";
    }

}
