using MemcachedProject.Models;

namespace MemcachedProject.Repositories;

public interface IErrorRepository
{
    Task<Errlog> GetErrorByIdAsync(int id);
    Task<string> AddErrorAsync(Errlog product);
    Task<string> UpdateErrorAsync(Errlog errlog);
    Task<string> DeleteErrorAsync(int guid);
    Task<string> GetAllErrorAsync(int count);

}
