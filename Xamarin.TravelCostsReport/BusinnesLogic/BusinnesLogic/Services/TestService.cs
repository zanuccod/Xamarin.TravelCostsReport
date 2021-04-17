using System;
using System.Collections.Generic;
using BusinnesLogic.Repository;

namespace BusinnesLogic.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;

        public TestService(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public IEnumerable<int> GetAll()
        {
            return testRepository.GetAll();
        }
    }
}
