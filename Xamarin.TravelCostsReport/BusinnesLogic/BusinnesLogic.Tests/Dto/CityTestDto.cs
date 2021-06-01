using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinnesLogic.Dto;
using Xunit;

namespace BusinnesLogic.Tests.Dto
{
    public class CityTestDto
    {
        [Fact]
        public void EqualsToNull_ShouldReturnFalse()
        {
            // Arrange
            var city = new CityDto();

            // Act
            var result = city.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToAnotherObjectType_ShouldReturnFalse()
        {
            // Arrange
            var city = new CityDto();

            // Act
            var result = city.Equals(new object());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToUserWithSameProperties_ShouldReturnTrue()
        {
            // Arrange
            var cityItems = new List<CityItemDto>
            {
                new CityItemDto
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItemDto
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new CityDto { Name = "cityName", CityItems = cityItems };
            var city_1 = new CityDto { Name = "cityName", CityItems = cityItems };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsToUserWithDifferentNames_ShouldReturnFalse()
        {
            // Arrange
            var cityItems = new List<CityItemDto>
            {
                new CityItemDto
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItemDto
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new CityDto { Name = "cityName", CityItems = cityItems };
            var city_1 = new CityDto { Name = "cityName_1", CityItems = cityItems };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToUserWithDifferentCityItemsNames_ShouldReturnFalse()
        {
            // Arrange
            var cityItems = new List<CityItemDto>
            {
                new CityItemDto
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItemDto
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new CityDto { Name = "cityName", CityItems = cityItems };
            var cityItems_1 = new List<CityItemDto>
            {
                new CityItemDto
                {
                    Name = "cityItem_2",
                    Distance = 20
                },
                new CityItemDto
                {
                    Name = "cityItem_3",
                    Distance = 30
                }
            };
            var city_1 = new CityDto { Name = "cityName_1", CityItems = cityItems_1 };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsToUserWithDifferentCityItemsDistances_ShouldReturnFalse()
        {
            // Arrange
            var cityItems = new List<CityItemDto>
            {
                new CityItemDto
                {
                    Name = "cityItem_1",
                    Distance = 20
                },
                new CityItemDto
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city = new CityDto { Name = "cityName", CityItems = cityItems };
            var cityItems_1 = new List<CityItemDto>
            {
                new CityItemDto
                {
                    Name = "cityItem_1",
                    Distance = 40
                },
                new CityItemDto
                {
                    Name = "cityItem_2",
                    Distance = 30
                }
            };
            var city_1 = new CityDto { Name = "cityName_1", CityItems = cityItems_1 };

            // Act
            var result = city.Equals(city_1);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(0, null, true)]
        [InlineData(1, null, false)]
        [InlineData(1, "pippo", false)]
        public void IsNull(int id, string name, bool expectedResult)
        {
            // Arrange
            var item = new CityDto()
            {
                Id = id,
                Name = name
            };

            // Act
            var result = item.IsEmpty();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(new int[] { }, 1, new int[] { 1 })]
        [InlineData(new int[] { 1 }, 2, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2 }, 3, new int[] { 1, 2, 3 })]
        public void AddTravelStep(int[] startState, int travelStep, int[] expectedResult)
        {
            // Arrange
            var travelSteps = new Collection<int>();
            foreach (var step in startState)
            {
                travelSteps.Add(step);
            }
            var item = new CityDto(travelSteps);

            // Act
            item.AddTravelStep(travelStep);

            // Assert
            Assert.Equal(expectedResult, item.TravelSteps);
        }

        [Theory]
        [InlineData(new int[] { }, new int[] { })]
        [InlineData(new int[] { 2 }, new int[] { })]
        [InlineData(new int[] { 2, 3 }, new int[] { 2 })]
        [InlineData(new int[] { 2, 3, 4 }, new int[] { 2, 3 })]
        public void RemoveLastTravelStep(int[] index, int[] expectedResult)
        {
            // Arrange
            var item = new CityDto();
            foreach(var step in index)
            {
                item.AddTravelStep(step);
            }

            // Act
            item.RemoveLastTravelStep();

            // Assert
            Assert.Equal(expectedResult, item.TravelSteps);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        [InlineData("Pippo_10", 0)]
        [InlineData("Pippo_1", 20)]
        public void GetDistanceTo(string targetCity, float expectedResult)
        {
            // Arrange
            var city = new CityDto()
            {
                CityItems = new List<CityItemDto>()
                {
                    new CityItemDto()
                    {
                        Name = "Pippo",
                        Distance = 10
                    },
                    new CityItemDto()
                    {
                        Name = "Pippo_1",
                        Distance = 20
                    },
                    new CityItemDto()
                    {
                        Name = "Pippo_2",
                        Distance = 30
                    }
                }
            };


            // Act
            var result = city.GetDistanceTo(targetCity);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
