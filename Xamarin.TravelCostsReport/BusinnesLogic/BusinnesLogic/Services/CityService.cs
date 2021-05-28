using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinnesLogic.Dto;
using BusinnesLogic.Models;
using BusinnesLogic.Repository;
using LiteDB;
using Serilog;

namespace BusinnesLogic.Services
{
    public class CityService : ICityService
    {
        private readonly IDataStore<City> cityRepository;
        private readonly IMapper mapper;

        public CityService(IDataStore<City> cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public Task<int> DeleteAllAsync()
        {
            Log.Debug("{method} delete all the items");
            return cityRepository.DeleteAllAsync();
        }

        public Task<bool> DeleteAsync(CityDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("unable to delete the given item because is null");
            }
            return cityRepository.DeleteAsync(mapper.Map<City>(item));
        }

        public async Task<IEnumerable<CityDto>> FindAllAsync()
        {
            return mapper.Map<IEnumerable<CityDto>>(await cityRepository.FindAllAsync());
        }

        public async Task<CityDto> FindByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException($"unable to find the item because gived id <{id}> is not valid");
            }
            return mapper.Map<CityDto>(await cityRepository.FindByIdAsync(id));
        }

        public Task<BsonValue> InsertAsync(CityDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("unable to insert given element because is null");
            }

            Log.Debug("{method} insert item with name <{cityName}>",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                item.Name);
            return cityRepository.InsertAsync(mapper.Map<City>(item));
        }

        public Task<int> InsertItemsAsync(IEnumerable<CityDto> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("unable to insert the given collection because is null");
            }

            Log.Debug("{method} add <{itemCount}> items",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                items.Count());

            return cityRepository.InsertItemsAsync(mapper.Map<IEnumerable<City>>(items));
        }

        public Task<bool> UpdateAsync(CityDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("unable to update the element because is null");
            }

            Log.Debug("{method} update item with id <{id}> name <{cityName}>",
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
                item.Id,
                item.Name);

            return cityRepository.UpdateAsync(mapper.Map<City>(item));
        }
    }
}
