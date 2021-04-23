using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinnesLogic.Models;

namespace Core.Presenters
{
    public interface IViewTestPresenter
    {
        public Task<IEnumerable<City>> GetItems();
    }
}
