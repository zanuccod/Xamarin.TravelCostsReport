using System;
using System.Collections.Generic;

namespace Core.Presenters
{
    public interface IViewTestPresenter
    {
        public IEnumerable<int> GetItems();
    }
}
