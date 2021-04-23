using System;
using System.Collections.Generic;
using BusinnesLogic.Models;

namespace Core.IViews
{
    public interface IViewTest
    {
        public IEnumerable<City> Test();
        public int GetOneValue();
        public void PressOkBtn();
    }
}
