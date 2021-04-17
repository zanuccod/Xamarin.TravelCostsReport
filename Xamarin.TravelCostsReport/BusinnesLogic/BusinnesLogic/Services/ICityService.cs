using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;

namespace BusinnesLogic.Services
{
    public interface ICityService
    {
        public Task<IEnumerable<City>> FindAll();
        public Task InsertCities(IEnumerable<City> items);
    }
}
