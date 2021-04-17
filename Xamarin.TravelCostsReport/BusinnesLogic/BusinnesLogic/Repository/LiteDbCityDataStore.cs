using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using LiteDB;

namespace BusinnesLogic.Repository
{
    public class LiteDbCityDataStore : LiteDbBase, IDataStore<City>
    {
        private readonly LiteCollection<City> cities;

        public LiteDbCityDataStore(string dbPath = null)
            : base(dbPath)
        {
            // create table if not exist
            cities = (LiteCollection<City>)db.GetCollection<City>();

            // create index on Id key
            cities.EnsureIndex("Id");
        }

        public Task InsertAsync(City item)
        {
            return Task.FromResult(cities.Insert(item));
        }

        public Task InsertItemsAsync(IEnumerable<City> items)
        {
            return Task.FromResult(cities.Insert(items));
        }

        public Task DeleteAllAsync()
        {
            return Task.FromResult(cities.DeleteAll());
        }

        public Task DeleteAsync(City item)
        {
            return Task.FromResult(cities.Delete(item.Id));
        }

        public Task<IEnumerable<City>> FindAllAsync()
        {
            return Task.FromResult(cities.FindAll());
        }

        public Task<City> FindAsync(City item)
        {
            return Task.FromResult(cities.FindById(item.Id));
        }

        public Task UpdateAsync(City item)
        {
            return Task.FromResult(cities.Update(item));
        }
    }
}
