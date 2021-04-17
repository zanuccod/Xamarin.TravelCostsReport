using System;
using System.Collections.Generic;

namespace BusinnesLogic.Repository
{
    public interface ITestRepository
    {
        IEnumerable<int> GetAll();
    }
}
