using Fac.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fac.Services
{
    public interface IDataStore
    {
        Task<bool> AddCaseAsync(Case item);
        //Task<bool> UpdateCaseAsync(T item);
        //Task<bool> DeleteCaseAsync(string id);
        //Task<T> GetCaseAsync(string id);
        Task<IEnumerable<CaseSummary>> GetCasesAsync(bool forceRefresh = false);
        Task<CategoriesSummary> GetCategories(bool forceRefresh = false);
    }
}
