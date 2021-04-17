using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB;

namespace BusinnesLogic.Repository
{
    public interface IDataStore<T>
    {
        Task<BsonValue> InsertAsync(T item);
        Task<int> InsertItemsAsync(IEnumerable<T> items);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<T> FindAsync(T item);
        Task<IEnumerable<T>> FindAllAsync();
        Task<int> DeleteAllAsync();
    }
}
