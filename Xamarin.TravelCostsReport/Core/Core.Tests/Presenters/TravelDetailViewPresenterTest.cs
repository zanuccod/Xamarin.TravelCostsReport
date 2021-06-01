using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BusinnesLogic.Dto;
using BusinnesLogic.Services;
using Core.IViews;
using Core.Presenters;
using Moq;
using Xunit;

namespace Core.Tests.Presenters
{
    public class TravelDetailViewPresenterTest
    {
        private readonly TravelDetailViewPresenter presenter;
        private readonly Mock<ICityService> mockCityService;
        private readonly Mock<ITravelDetailView> mockView;

        public TravelDetailViewPresenterTest()
        {
            mockCityService = new Mock<ICityService>();
            mockView = new Mock<ITravelDetailView>();

            presenter = new TravelDetailViewPresenter(mockView.Object, mockCityService.Object);
        }

        [Fact]
        public void AddTravelStep_FirstSelection_ShouldAddOnlySelectionToHistory()
        {
            // Arrange
            presenter.Items = GetDummyData();
            var itemIndex = 2;

            // Act
            presenter.AddTravelStep(itemIndex);

            // Assert
            Assert.Equal(0, presenter.TravelTotalDistance);
            Assert.Equal(2, presenter.NextTravelStepIndex);
            Assert.Equal(1, presenter.SelectionHistory.Count);
            Assert.Equal(TravelDetailViewPresenter.START_TRAVEL_STEP_INDEX, presenter.Items.ElementAt(itemIndex).TravelSteps.First());
            Assert.Equal(itemIndex, presenter.SelectionHistory.First());
        }

        [Fact]
        public void AddTravelStep_SecondSelection_ShouldAddSelectionToHistoryAndDistance()
        {
            // Arrange
            presenter.Items = GetDummyData();
            presenter.AddTravelStep(1); 

            // travel distance from city_2 to city_3 => 30
            var itemIndex = 2;

            // Act
            presenter.AddTravelStep(itemIndex);

            // Assert
            Assert.Equal(presenter.Items.ElementAt(1).CityItems.ElementAt(itemIndex).Distance, presenter.TravelTotalDistance);
            Assert.Equal(3, presenter.NextTravelStepIndex);
            Assert.Equal(2, presenter.SelectionHistory.Count);
            Assert.Equal(TravelDetailViewPresenter.START_TRAVEL_STEP_INDEX, presenter.Items.ElementAt(1).TravelSteps.First());
            Assert.Equal(TravelDetailViewPresenter.START_TRAVEL_STEP_INDEX + 1, presenter.Items.ElementAt(itemIndex).TravelSteps.First());
            Assert.Equal(itemIndex, presenter.SelectionHistory.Last());
        }

        [Fact]
        public void RemoveTravelStep_NoPreviousSelection_NothingToDo()
        {
            // Act
            presenter.Items = GetDummyData();
            presenter.RemoveTravelStep(1);

            // Assert
            Assert.Equal(0, presenter.TravelTotalDistance);
            Assert.Equal(TravelDetailViewPresenter.START_TRAVEL_STEP_INDEX, presenter.NextTravelStepIndex);
            Assert.Equal(0, presenter.SelectionHistory.Count);
        }

        [Theory]
        [InlineData(1, 1, 0, TravelDetailViewPresenter.START_TRAVEL_STEP_INDEX, 0)]
        [InlineData(1, 2, 0, 2, 1)] // try to remove a different step from the only one selected => nothing to remove
        [InlineData(2, 1, 0, 2, 1)] // try to remove a different step from the only one selected => nothing to remove
        public void RemoveTravelStep_OnlyOnePreviousSelection(
            int stepToAdd,
            int stepToRemove,
            int expectedDistance,
            int expectedNextTravelIndex,
            int expectedSelectionHistoryCount)
        {
            // Arrange
            presenter.Items = GetDummyData();
            presenter.AddTravelStep(stepToAdd);

            // Act
            presenter.RemoveTravelStep(stepToRemove);

            // Assert
            Assert.Equal(expectedDistance, presenter.TravelTotalDistance);
            Assert.Equal(expectedNextTravelIndex, presenter.NextTravelStepIndex);
            Assert.Equal(expectedSelectionHistoryCount, presenter.SelectionHistory.Count);
        }

        [Fact]
        public void RemoveTravelStep_RemovedLastSelection_ShouldSubtrackTheDistance()
        {
            // Arrange
            presenter.Items = GetDummyData();
            presenter.AddTravelStep(0);
            presenter.AddTravelStep(2); // total_distance 30
            presenter.AddTravelStep(3); // total_distance 70
            presenter.AddTravelStep(0); // total_distance 80

            // Act
            presenter.RemoveTravelStep(0);

            // Assert
            Assert.Equal(70, presenter.TravelTotalDistance);
            Assert.Equal(4, presenter.NextTravelStepIndex);
            Assert.Equal(3, presenter.SelectionHistory.Count);

            Assert.Equal(1, presenter.Items.ElementAt(0).TravelSteps.First());
            Assert.Equal(2, presenter.Items.ElementAt(2).TravelSteps.First());
            Assert.Equal(3, presenter.Items.ElementAt(3).TravelSteps.First());
        }

        [Fact]
        public void LoadItemsComma_ReadTwoElements_Success()
        {
            // Arrange
            var expectedData = GetDummyData();
            mockCityService
                .Setup(x => x.FindAllAsync())
                .ReturnsAsync(expectedData);

            // Act
            presenter.LoadItemsCommand.Execute(null);

            // Assert
            Assert.Equal(expectedData, presenter.Items);
        }

        /// <summary>
        /// If LoadItemsCommand don't stop when isBusy is true delete viewModel.Items of two elements
        /// and load list on 1 element.
        /// Finally Assert failed.
        /// </summary>
        [Fact]
        public void LoadItemsCommand_IsBusy_NothingToDo()
        {
            // Arrange
            presenter.IsBusy = true;

            var expectedData = GetDummyData();
            mockCityService
                .Setup(x => x.FindAllAsync())
                .ReturnsAsync(expectedData);

            presenter.Items = new List<CityDto>()
            {
                new CityDto(),
                new CityDto()
            };

            // Act
            presenter.LoadItemsCommand.Execute(null);

            // Assert
            Assert.Equal(2, presenter.Items.Count());
        }

        private IEnumerable<CityDto> GetDummyData()
        {
            return new List<CityDto>()
            {
                // 0
                new CityDto()
                {
                    Name = "City_1",
                    CityItems = new Collection<CityItemDto>()
                    {
                        new CityItemDto() { Name = "City_1", Distance = 2 },
                        new CityItemDto() { Name = "City_2", Distance = 20 },
                        new CityItemDto() { Name = "City_3", Distance = 30 },
                        new CityItemDto() { Name = "City_4", Distance = 40 },
                    }
                },
                // 1
                new CityDto()
                {
                    Name = "City_2",
                    CityItems = new Collection<CityItemDto>()
                    {
                        new CityItemDto() { Name = "City_1", Distance = 10 },
                        new CityItemDto() { Name = "City_2", Distance = 2 },
                        new CityItemDto() { Name = "City_3", Distance = 30 },
                        new CityItemDto() { Name = "City_4", Distance = 40 },
                    }
                },
                // 2
                new CityDto()
                {
                    Name = "City_3",
                    CityItems = new Collection<CityItemDto>()
                    {
                        new CityItemDto() { Name = "City_1", Distance = 10 },
                        new CityItemDto() { Name = "City_2", Distance = 20 },
                        new CityItemDto() { Name = "City_3", Distance = 2 },
                        new CityItemDto() { Name = "City_4", Distance = 40 },
                    }
                },
                // 3
                new CityDto()
                {
                    Name = "City_4",
                    CityItems = new Collection<CityItemDto>()
                    {
                        new CityItemDto() { Name = "City_1", Distance = 10 },
                        new CityItemDto() { Name = "City_2", Distance = 20 },
                        new CityItemDto() { Name = "City_3", Distance = 30 },
                        new CityItemDto() { Name = "City_4", Distance = 2 },
                    }
                }
            };
        }
    }
}
