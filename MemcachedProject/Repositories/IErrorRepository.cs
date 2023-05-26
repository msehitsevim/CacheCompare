using Entities;

namespace MemcachedProject.Repositories;

public interface IErrorRepository
{
    Task<ErrorLog> GetErrorByIdAsync(int id);
    Task<string> AddErrorAsync(ErrorLog product);
    Task<string> UpdateErrorAsync(ErrorLog errlog);
    Task<string> DeleteErrorAsync(int guid);
    Task<string> GetAllErrorAsync(int count);

}
