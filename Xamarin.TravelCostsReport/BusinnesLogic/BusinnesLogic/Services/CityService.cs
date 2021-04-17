using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using BusinnesLogic.Repository;

namespace BusinnesLogic.Services
{
    public class CityService : ICityService
    {
        private readonly IDataStore<City> travelRepository;

        public CityService(IDataStore<City> travelRepository)
        {
            this.travelRepository = travelRepository;
        }

        public Task<IEnumerable<City>> FindAll()
        {
            return travelRepository.FindAllAsync();
        }

        public Task InsertCities(IEnumerable<City> items)
        {
            return travelRepository.InsertItemsAsync(items);
        }
    }
}
