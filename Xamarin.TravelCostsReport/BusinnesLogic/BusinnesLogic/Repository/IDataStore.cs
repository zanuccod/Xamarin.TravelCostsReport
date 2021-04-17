using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinnesLogic.Repository
{
    public interface IDataStore<T>
    {
        Task InsertAsync(T item);
        Task InsertItemsAsync(IEnumerable<T> items);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task<T> FindAsync(T item);
        Task<IEnumerable<T>> FindAllAsync();
        Task DeleteAllAsync();
    }
}
