using BusinnesLogic.Models;
using Xunit;

namespace BusinnesLogic.Tests.Models
{
    public class CityItemDtoTest
    {
        [Fact]
        public void EqualsToNull_ShouldReturnFalse()
        {
            // Arrange
            var cityItem = new CityItem();

            // Act
            var result = cityItem.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToAnotherObjectType_ShouldReturnFalse()
        {
            // Arrange
            var cityItem = new CityItem();

            // Act
            var result = cityItem.Equals(new object());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToObjectWithSameValues_ShouldReturnTrue()
        {
            // Arrange
            var cityItem = new CityItem();
            var cityItem_1 = new CityItem();

            // Act
            var result = cityItem.Equals((object)cityItem_1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsToUserWithSameProperties_ShouldReturnTrue()
        {
            // Arrange
            var cityItem = new CityItem { Name = "cityItem", Distance = 10 };
            var cityItem_1 = new CityItem { Name = "cityItem", Distance = 10 };

            // Act
            var result = cityItem.Equals(cityItem_1);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("name", 10, "name1", 10)]
        [InlineData("name", 10, "name", 20)]
        public void EqualsToUserWithDifferentProperties_ShouldReturnFalse(string name, int distance, string name1, int distance1)
        {
            // Arrange
            var cityItem = new CityItem { Name = name, Distance = distance };
            var cityItem_1 = new CityItem { Name = name1, Distance = distance1 };

            // Act
            var result = cityItem.Equals(cityItem_1);

            // Assert
            Assert.False(result);
        }
    }
}
