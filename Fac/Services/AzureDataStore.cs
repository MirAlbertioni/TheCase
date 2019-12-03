using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Fac.Models;
using System.Net.Http.Headers;
using System.Linq;

namespace Fac.Services
{
    public class AzureDataStore : IDataStore
    {
        HttpClient client;
        IEnumerable<CaseSummary> cases;
        CategoriesSummary categories;

        public AzureDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };

            cases = new List<CaseSummary>();
            categories = new CategoriesSummary();
        }

        public async Task<IEnumerable<CaseSummary>> GetCasesAsync(bool forceRefresh = false)
        {
            try
            {
                var json = await client.GetStringAsync($"api/Case/GetCases/");
                cases = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<CaseSummary>>(json));
            }
            catch (Exception e)
            {

            }

            return cases.ToList();
        }

        public async Task<CategoriesSummary> GetCategories(bool forceRefresh = false)
        {
            try
            {
                var json = await client.GetStringAsync($"api/Category/GetCategories/");
                categories = await Task.Run(() => JsonConvert.DeserializeObject<CategoriesSummary>(json));
            }
            catch (Exception e)
            {

            }

            return categories;
        }

        public async Task<bool> AddCaseAsync(Case item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync($"api/Case/AddCase/", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        //public async Task<bool> UpdateCaseAsync(Case item)
        //{
        //    if (item == null)
        //        return false;

        //    var serializedItem = JsonConvert.SerializeObject(item);
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await client.PostAsync($"api/Case/UpdateCase/", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

        //    return response.IsSuccessStatusCode;
        //}

        //public async Task<bool> DeleteCaseAsync(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //        return false;

        //    var response = await client.DeleteAsync($"api/item/{id}");

        //    return response.IsSuccessStatusCode;
        //}
    }
}