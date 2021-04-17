using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using BusinnesLogic.Repository;

namespace BusinnesLogic.Services
{
    public class TravelReportService : ITravelReportService
    {
        private readonly IDataStore<City> travelRepository;

        public TravelReportService(IDataStore<City> travelRepository)
        {
            this.travelRepository = travelRepository;
        }

        public Task<IEnumerable<City>> FindAll()
        {
            return travelRepository.FindAllAsync();
        }
    }
}
