using ProjectManagementSystem.Client.Services;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProjectManagementSystem.Shared.Models;

namespace ProjectManagementSystem.Client.Services
{
    public class GenericRepoService
    {
        public class GenericRepositoryService : IGenericRepositoryService
        {
            private readonly HttpClient _httpClient;

            public GenericRepositoryService(HttpClient Client)
            {
                _httpClient = Client;
            }

            public async Task<IEnumerable<T>> GetAllAsync<T>(string url) where T : class
            {
                try
                {
                    var res = await _httpClient.GetFromJsonAsync<List<T>>(url);
                    return res;
                }
                catch (Exception ex)

                {

                    var message = ex.Message;
                    throw;
                }
            }


            public async Task<T> SaveAllAsync<T>(string url, T value)
            {

                try
                {
                    var result = await _httpClient.PostAsJsonAsync(url, value);
                    return await result.Content.ReadFromJsonAsync<T>();
                }
                catch (Exception ex)
                {
                    var _ = ex.Message;
                    throw;
                }
                


            }


            public async Task<T> GetByIdAsync<T>(string url) where T : class, new()
            {
                try
                {
                    return await _httpClient.GetFromJsonAsync<T>(url);
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw;
                }
            }

            public async Task<T> UpdateAsync<T>(string url, T value) where T : class
            {
                try
                {
                    var updatedata = await _httpClient.PutAsJsonAsync(url, value);

                    return await updatedata.Content.ReadFromJsonAsync<T>();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw;
                }
            }

            public async Task<bool> DeleteAsync(string api)
            {
                try
                {
                    var deletedata = await _httpClient.DeleteAsync(api);
                    //deletedata.EnsureSuccessStatusCode();
                    var datadeleted = await deletedata.Content.ReadFromJsonAsync<bool>();

                    return datadeleted;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw;
                }
            }


            public async Task<T> GetAllReturnModelAsync<T>(string url) where T : class
            {
                try
                {
                    return await _httpClient.GetFromJsonAsync<T>(url);
                }
                catch (Exception ex)

                {
                    var _ = ex.Message;
                    throw;
                }
            }

            public async Task<string> GetStringAsync(string url)
            {
                try
                {
                    return await _httpClient.GetStringAsync(url);
                }
                catch (Exception ex)
                {
                    var _ = ex.Message;
                    throw;
                }
            }

        }
    }
}













