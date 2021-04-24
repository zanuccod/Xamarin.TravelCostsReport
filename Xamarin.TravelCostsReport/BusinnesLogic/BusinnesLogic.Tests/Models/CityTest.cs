using System.Collections.Generic;
using BusinnesLogic.Models;
using Xunit;

namespace BusinnesLogic.Tests.Models
{
    public class CityTest
    {
        [Fact]
        public void EqualsToNull_ShouldReturnFalse()
        {
            // Arrange
            var city = new City();

            // Act
            var result = city.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToAnotherObjectType_ShouldReturnFalse()
        {
            // Arrange
            var city = new City();

            // Act
            var result = city.Equals(new object());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToUserWithSameProperties_ShouldReturnTrue()
        {
            // Arrange
            var cityItems = new List<CityItem>
            {
                new CityItem
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItem
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new City { Name = "cityName", CityItems = cityItems };
            var city_1 = new City { Name = "cityName", CityItems = cityItems };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsToUserWithDifferentNames_ShouldReturnFalse()
        {
            // Arrange
            var cityItems = new List<CityItem>
            {
                new CityItem
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItem
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new City { Name = "cityName", CityItems = cityItems };
            var city_1 = new City { Name = "cityName_1", CityItems = cityItems };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToUserWithDifferentCityItemsNames_ShouldReturnFalse()
        {
            // Arrange
            var cityItems = new List<CityItem>
            {
                new CityItem
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItem
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new City { Name = "cityName", CityItems = cityItems };
            var cityItems_1 = new List<CityItem>
            {
                new CityItem
                {
                    Name = "cityItem_2",
                    Distance = 20
                },
                new CityItem
                {
                    Name = "cityItem_3",
                    Distance = 30
                }
            };
            var city_1 = new City { Name = "cityName_1", CityItems = cityItems_1 };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToUserWithDifferentCityItemsDistances_ShouldReturnFalse()
        {
            // Arrange
            var cityItems = new List<CityItem>
            {
                new CityItem
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItem
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new City { Name = "cityName", CityItems = cityItems };
            var cityItems_1 = new List<CityItem>
            {
                new CityItem
                {
                    Name = "cityItem_1",
                    Distance = 40
                },
                new CityItem
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city_1 = new City { Name = "cityName_1", CityItems = cityItems_1 };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.False(result);
        }
    }
}
