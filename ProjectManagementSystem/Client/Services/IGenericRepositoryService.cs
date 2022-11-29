namespace ProjectManagementSystem.Client.Services
{
    public interface IGenericRepositoryService
    {
        Task<bool> DeleteAsync(string api);
        Task<IEnumerable<T>> GetAllAsync<T>(string url) where T : class;
        Task<T> GetAllReturnModelAsync<T>(string url) where T : class;
        Task<T> GetByIdAsync<T>(string url) where T : class, new();
        Task<string> GetStringAsync(string url);
        Task<T> SaveAllAsync<T>(string url, T value);
        Task<T> UpdateAsync<T>(string url, T value) where T : class;
    }
}