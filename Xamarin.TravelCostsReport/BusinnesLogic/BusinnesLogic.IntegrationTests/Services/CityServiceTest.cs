using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using BusinnesLogic.Repository;
using BusinnesLogic.Services;
using Xunit;

namespace BusinnesLogic.IntegrationTests.Services
{
    public class CityServiceTest : IDisposable
    {
        private readonly IDataStore<City> dataStore;
        private readonly ICityService service;

        private const string dbName = "testDataStore";

        public CityServiceTest()
        {
            dataStore = new LiteDbCityDataStore(dbName);
            service = new CityService(dataStore);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddTheElement()
        {
            // Arrange
            var item = new City()
            {
                Name = "name",
                CityItems = new List<CityItem>()
                {
                    new CityItem()
                    {
                        Name = "pippo",
                        Distance = 10
                    }
                }
            };

            // Act
            await service.InsertAsync(item);

            // Assert
            var insertedItem = await service.FindAllAsync();
            Assert.True(item.Equals(insertedItem.First()));
        }

        [Fact]
        public async Task InsertAsync_NullElement_ShouldThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertAsync(null));
        }

        [Fact]
        public async Task InsertItemsAsync_ShouldInsertAllTheElements()
        {
            // Arrange
            var items = new List<City>()
            {
                new City()
                {
                    Name = "name",
                    CityItems = new List<CityItem>()
                    {
                        new CityItem()
                        {
                            Name = "pippo",
                            Distance = 10
                        }
                    }
                },
                new City()
                {
                    Name = "name-1",
                    CityItems = new List<CityItem>()
                    {
                        new CityItem()
                        {
                            Name = "pippo_1",
                            Distance = 20
                        }
                    }
                }
            };

            // Act
            var result = await service.InsertItemsAsync(items);

            // Assert
            Assert.Equal(items.Count, result);
            var insertedItem = await service.FindAllAsync();
            Assert.True(items.First().Equals(insertedItem.First()));
            Assert.True(items.ElementAt(1).Equals(insertedItem.ElementAt(1)));
        }

        [Fact]
        public async Task InsertItemsAsync_NullElements_ShouldThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertItemsAsync(null));
        }

        [Fact]
        public async Task FindAsync_ElementExist_ShouldReturnTheElement()
        {
            // Arrange
            var item = new City()
            {
                Name = "name",
                CityItems = new List<CityItem>()
                {
                    new CityItem()
                    {
                        Name = "pippo",
                        Distance = 10
                    }
                }
            };
            await service.InsertAsync(item);
            var insertedItemm = await service.FindAllAsync();

            // Act
            var result = await service.FindByIdAsync(insertedItemm.First().Id);

            // Assert
            Assert.NotNull(result);
            Assert.True(item.Equals(result));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public async Task FindAsync_InvalidID_ShouldThrowArgumentException(int id)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => service.FindByIdAsync(id));

        }

        [Fact]
        public async Task UpdateAsync_ExistingElement_ShouldUpdateElementAndReturnTrue()
        {
            // Arrange
            var item = new City()
            {
                Name = "name",
                CityItems = new List<CityItem>()
                {
                    new CityItem()
                    {
                        Name = "pippo",
                        Distance = 10
                    }
                }
            };
            await service.InsertAsync(item);
            var insertedItem = await service.FindAllAsync();

            insertedItem.ElementAt(0).Name = "name_1";

            // Act
            var result = await service.UpdateAsync(insertedItem.First());

            // Assert
            Assert.True(result);
            var updatedElement = await service.FindAllAsync();
            Assert.True(insertedItem.ElementAt(0).Name.Equals(updatedElement.ElementAt(0).Name));
        }

        [Fact]
        public async Task UpdateAsync_ElementNotExist_ShouldDoNothing()
        {
            // Act
            var result = await service.UpdateAsync(new City() { Id = 1 });

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_NullElements_ShouldThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(null));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteAnExistingElement()
        {
            // Arrange
            var item = new City()
            {
                Name = "name",
                CityItems = new List<CityItem>()
                {
                    new CityItem()
                    {
                        Name = "pippo",
                        Distance = 10
                    }
                }
            };
            await service.InsertAsync(item);
            var insertedItem = await service.FindAllAsync();

            // Act
            var result = await service.DeleteAsync(insertedItem.First());

            // Assert
            Assert.True(result);
            var updatedElement = await service.FindAllAsync();
            Assert.True(updatedElement.Count() == 0);
        }

        [Fact]
        public async Task DeleteAsync_NullElements_ShouldThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(null));
        }

        [Fact]
        public async Task DeleteAllAsync_ShouldDeleteAllTheElements()
        {
            // Arrange
            var item = new City()
            {
                Name = "name",
                CityItems = new List<CityItem>()
                {
                    new CityItem()
                    {
                        Name = "pippo",
                        Distance = 10
                    }
                }
            };
            await service.InsertAsync(item);

            // Act
            var result = await service.DeleteAllAsync();

            // Assert
            Assert.Equal(1, result);
            var updatedElement = await service.FindAllAsync();
            Assert.False(updatedElement.Any());
        }

        public void Dispose()
        {
            // delete all database files generated for test
            var files = Directory.GetFiles(Path.GetDirectoryName(Path.GetFullPath(dbName)), string.Concat(dbName, "-*"));
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
