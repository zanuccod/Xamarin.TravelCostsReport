using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using BusinnesLogic.Repository;
using LiteDB;
using Serilog;

namespace BusinnesLogic.Services
{
    public class CityService : ICityService
    {
        private readonly IDataStore<City> cityRepository;

        public CityService(IDataStore<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public Task<int> DeleteAllAsync()
        {
            Log.Debug("{method} delete all the items");
            return cityRepository.DeleteAllAsync();
        }

        public Task<bool> DeleteAsync(City item)
        {
            if (item == null)
            {
                throw new Exception("unable to delete the given item because is null");
            }
            return cityRepository.DeleteAsync(item);
        }

        public Task<IEnumerable<City>> FindAllAsync()
        {
            return cityRepository.FindAllAsync();
        }

        public Task<City> FindAsync(City item)
        {
            if (item == null)
            {
                throw new Exception("unable to find the given item because is null");
            }
            return cityRepository.FindAsync(item);
        }

        public Task<BsonValue> InsertAsync(City item)
        {
            if (item == null)
            {
                throw new Exception("unable to insert the given item because is null");
            }
            return cityRepository.InsertAsync(item);
        }

        public Task InsertCityAsync(City item)
        {
            if (item == null)
            {
                throw new Exception("unable to insert the element because is null");
            }

            Log.Debug("{method} insert item with name <{cityName}>",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                item.Name);
            return cityRepository.InsertAsync(item);
        }

        public Task<int> InsertItemsAsync(IEnumerable<City> items)
        {
            if (items == null)
            {
                throw new Exception("unable to insert the given collection because is null");
            }

            Log.Debug("{method} add <{itemCount}> items",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                items.Count());

            return cityRepository.InsertItemsAsync(items);
        }

        public Task<bool> UpdateAsync(City item)
        {
            if (item == null)
            {
                throw new Exception("unable to update the element because is null");
            }

            Log.Debug("{method} update item with id <{id}> name <{cityName}>",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                item.Id,
                item.Name);

            return cityRepository.UpdateAsync(item);
        }
    }
}
