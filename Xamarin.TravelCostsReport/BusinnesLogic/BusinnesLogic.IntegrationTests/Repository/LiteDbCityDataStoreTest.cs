using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinnesLogic.Models;
using BusinnesLogic.Repository;
using Xunit;

namespace BusinnesLogic.IntegrationTests.Repository
{
    public class LiteDbCityDataStoreTest : IDisposable
    {
        private LiteDbCityDataStore dataStore;
        private const string dbName = "testDataStore";

        public LiteDbCityDataStoreTest()
        {
            dataStore = new LiteDbCityDataStore(dbName);
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
            await dataStore.InsertAsync(item);

            // Assert
            var insertedItem = await dataStore.FindAllAsync();
            Assert.True(item.Equals(insertedItem.First()));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAnExistingElement()
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
            await dataStore.InsertAsync(item);
            var insertedItem = await dataStore.FindAllAsync();

            insertedItem.ElementAt(0).Name = "name_1";

            // Act
            await dataStore.UpdateAsync(insertedItem.First());

            // Assert
            var updatedElement = await dataStore.FindAllAsync();
            Assert.True(insertedItem.ElementAt(0).Name.Equals(updatedElement.ElementAt(0).Name));
        }

        [Fact]
        public async Task UpdateAsync_ElementNotExist_ShouldDoNothing()
        {
            // Act
            await dataStore.UpdateAsync(new City() { Id = 1 });
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
            await dataStore.InsertAsync(item);
            var insertedItem = await dataStore.FindAllAsync();

            // Act
            await dataStore.DeleteAsync(insertedItem.First());

            // Assert
            var updatedElement = await dataStore.FindAllAsync();
            Assert.True(updatedElement.Count() == 0);
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
            await dataStore.InsertAsync(item);

            // Act
            await dataStore.DeleteAllAsync();

            // Assert
            var updatedElement = await dataStore.FindAllAsync();
            Assert.True(updatedElement.Count() == 0);
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
