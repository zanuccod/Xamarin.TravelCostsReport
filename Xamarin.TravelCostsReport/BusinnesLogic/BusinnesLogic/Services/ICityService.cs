using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using LiteDB;

namespace BusinnesLogic.Services
{
    public interface ICityService
    {
        Task<BsonValue> InsertAsync(City item);
        Task<int> InsertItemsAsync(IEnumerable<City> items);
        Task<bool> UpdateAsync(City item);
        Task<bool> DeleteAsync(City item);
        Task<City> FindAsync(City item);
        Task<IEnumerable<City>> FindAllAsync();
        Task<int> DeleteAllAsync();
    }
}
