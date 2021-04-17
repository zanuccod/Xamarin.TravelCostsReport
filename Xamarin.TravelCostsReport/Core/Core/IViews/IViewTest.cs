using System;
using System.Collections.Generic;

namespace Core.IViews
{
    public interface IViewTest
    {
        public IEnumerable<int> Test();
        public int GetOneValue();
        public void PressOkBtn();
    }
}
