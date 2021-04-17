using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using LiteDB;

namespace BusinnesLogic.Repository
{
    public class LiteDbTravelReportDataStore : LiteDbBase, IDataStore<City>
    {
        private readonly LiteCollection<City> items;

        public LiteDbTravelReportDataStore(string dbPath = null)
            : base(dbPath)
        {
            // create table if not exist
            items = (LiteCollection<City>)db.GetCollection<City>();

            // create index on Id key
            items.EnsureIndex("Id");
        }

        public Task InsertAsync(City item)
        {
            return Task.Run(() => items.Insert(item));
        }

        public Task DeleteAllAsync()
        {
            return Task.Run(() => items.DeleteAll());
        }

        public Task DeleteAsync(City item)
        {
            return Task.Run(() => items.Delete(item.Id));
            throw new NotImplementedException();
        }

        public Task<IEnumerable<City>> FindAllAsync()
        {
            return Task.Run(() => items.FindAll());
        }

        public Task<City> FindAsync(City item)
        {
            return Task.Run(() => items.FindById(item.Id));
        }

        public Task UpdateAsync(City item)
        {
            return Task.Run(() => items.Update(item));
        }
    }
}
