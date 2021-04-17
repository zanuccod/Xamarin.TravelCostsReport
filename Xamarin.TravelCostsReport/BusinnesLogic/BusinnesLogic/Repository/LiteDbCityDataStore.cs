using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using LiteDB;
using Serilog;

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

        /// <summary>
        /// Insert a new entity to this collection. Document Id must be a new value in collection - Returns document Id
        /// </summary>
        public Task<BsonValue> InsertAsync(City item)
        {
            return Task.FromResult(cities.Insert(item));
        }

        /// <summary>
        /// Insert an array of new documents to this collection.
        /// Document Id must be a new value in collection.
        /// Can be set buffer size to commit at each N documents
        /// </summary>
        public Task<int> InsertItemsAsync(IEnumerable<City> items)
        {
            return Task.FromResult(cities.Insert(items));
        }

        /// <summary>
        /// Delete all documents inside collection.
        /// Returns how many documents was deleted.
        /// Run inside current transaction
        /// </summary>
        public Task<int> DeleteAllAsync()
        {
            return Task.FromResult(cities.DeleteAll());
        }

        /// <summary>
        /// Delete a single document on collection based on _id index.
        /// Returns true if document was deleted
        /// </summary>
        public Task<bool> DeleteAsync(City item)
        {
            return Task.FromResult(cities.Delete(item.Id));
        }

        /// <summary>
        /// Returns all documents inside collection order by _id index.
        /// </summary>
        public Task<IEnumerable<City>> FindAllAsync()
        {
            return Task.FromResult(cities.FindAll());
        }

        /// <summary>
        /// Find a document using Document Id.
        /// Returns null if not found.
        /// </summary>
        public Task<City> FindAsync(City item)
        {
            return Task.FromResult(cities.FindById(item.Id));
        }

        /// <summary>
        /// Update a document in this collection.
        /// Returns false if not found document in collection
        /// </summary>
        public Task<bool> UpdateAsync(City item)
        {
            return Task.FromResult(cities.Update(item));
        }
    }
}
