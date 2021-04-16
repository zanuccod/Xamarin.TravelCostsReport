using System;
using BusinnesLogic.Services;
using Xunit;

namespace BusinnesLogic.Tests
{
    public class ToBeTestedTests
    {
        [Theory]
        [InlineData("", "", "")]
        [InlineData("pippo", "", "pippo")]
        [InlineData("pippo", "pluto", "pippopluto")]
        public void Concat_ShouldConcatTwoStrings(string first, string second, string expectedResult)
        {
            // Act
            var result = ToBeTested.Concat(first, second);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
