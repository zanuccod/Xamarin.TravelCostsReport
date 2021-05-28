using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Dto;

namespace Core.Presenters
{
    public interface IViewTestPresenter
    {
        public Task<IEnumerable<CityDto>> GetItems();
    }
}
