using System;
using System.Collections.Generic;

namespace BusinnesLogic.Repository
{
    public class TestRepository : ITestRepository
    {
        public IEnumerable<int> GetAll()
        {
            return new List<int> { 1, 2, 3 };
        }
    }
}
