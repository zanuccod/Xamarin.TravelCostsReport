using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;

namespace BusinnesLogic.Services
{
    public interface ITravelReportService
    {
        public Task<IEnumerable<City>> FindAll();
    }
}
