using Enyim.Caching;
using MemcachedProject.Models;

namespace MemcachedProject.Repositories;

public class ErrorRepository : IErrorRepository
{
    private readonly IMemcachedClient _cache;
    private readonly CalismaVeriTabaniContext _context;

    public ErrorRepository(IMemcachedClient cache, CalismaVeriTabaniContext context)
    {
        _cache = cache;
        _context = context;
    }

    public async Task<string> GetErrorByIdAsync(int id)
    {
        var cacheKey = $"Error_{id}";

        var result = await _cache.GetAsync<Errlog>(cacheKey);

        if (result.Success)
        {
            return result.Value.Tarih.ToString()!;
        }

        return "Değer Bulunamadı.";
    }

    public async Task<string> AddErrorAsync(Errlog err)
    { 
        bool retval = await _cache.SetAsync($"Error_{err.Guid}", err, TimeSpan.FromMinutes(5));
        if (!retval)
        {
            return "Belleğe yazılamadı";
        }
        return "İşlem başarılı";
    }

    public async Task<string> UpdateErrorAsync(Errlog errlog)
    {
       bool retval = await _cache.ReplaceAsync($"error_{errlog.Guid}", errlog, TimeSpan.FromMinutes(5));
        if (!retval)
        {
            return "Bellekteki veri güncellenemedi";
        }
        return "İşlem başarılı";
    }

    public async Task<string> DeleteErrorAsync(int guid)
    {
        var result = await _cache.RemoveAsync($"error_{guid}");
        if (!result)
        {
            return "Veri Silinemedi.";
        }

        var getCacheData = await _cache.GetAsync<Errlog>($"error_{guid}");
        if (getCacheData != null)
        {
            return "Bellekte Veri Duruyor.";
        }

        return "İşlem başarılı.";
    }

    public async Task<string> GetAllErrorAsync()
    {
        List<Errlog> sqlData = _context.Errlogs.ToList();
        List<Errlog> resultList = new List<Errlog>();

        foreach (var item in sqlData)
        {
            await _cache.SetAsync($"Error_{item.Guid}", item, TimeSpan.FromMinutes(5));

        }
       return  "bitti";
    }


}
