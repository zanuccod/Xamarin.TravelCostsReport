using System;
using Xunit;

namespace Core.IntegrationTests
{
    public class ToBeTestedCoreTests
    {

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 1, 2)]
        public void Sum_ShouldSumTwoNumbers(int first, int second, int expectedResult)
        {
            // Act
            var result = ToBeTestedCore.Sum(first, second);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
