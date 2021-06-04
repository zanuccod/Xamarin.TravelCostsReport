﻿using System;
using System.Collections.Generic;
using BusinnesLogic.Services;
using BusinnesLogic.Dto;
using Core.Helpers;
using System.Threading.Tasks;
using System.Linq;
using Core.IViews;
using System.IO;
using BusinnesLogic.Helpers;
using System.Collections.ObjectModel;

namespace Core.Presenters
{
    public sealed class TravelDetailViewPresenter : BaseViewPresenter, IDisposable
    {
        #region Private Fields

        private bool _disposed = false;
        private readonly ICityService cityService;
        private readonly ITravelDetailView view;

        public const int START_TRAVEL_STEP_INDEX = 1;
        public const string FILE_NAME = "travelData.ods";

        #endregion

        #region Public Properties

        public IEnumerable<CityDto> Items { get; set; }
        public ICollection<int> SelectionHistory { get; private set; }
        public float TravelTotalDistance { get; private set; }
        public int NextTravelStepIndex { get; private set; }

        #endregion

        #region Commands

        public Command LoadItemsCommand { get; private set; }

        #endregion

        #region Constructors

        public TravelDetailViewPresenter(ITravelDetailView view, ICityService cityService)
        {
            this.view = view;
            this.cityService = cityService;

            Init();
        }

        #endregion

        #region Public Methods

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                Clear();
            }

            _disposed = true;
        }

        public void AddTravelStep(int position)
        {
            TravelTotalDistance += SelectionHistory.Any()
                ? GetDistanceFromTo(SelectionHistory.Last(), position)
                : 0;

            Items.ElementAt(position).AddTravelStep(NextTravelStepIndex);
            SelectionHistory.Add(position);
            NextTravelStepIndex++;
        }

        public void RemoveTravelStep(int position)
        {
            if (!SelectionHistory.Any() ||
                Items.ElementAt(position).TravelSteps.Count == 0 ||
                position != SelectionHistory.Last())
            {
                return;
            }

            TravelTotalDistance -= SelectionHistory.Count > 1
                ? GetDistanceFromTo(SelectionHistory.ElementAt(SelectionHistory.Count -2), position)
                : 0;

            Items.ElementAt(position).RemoveLastTravelStep();
            SelectionHistory.Remove(SelectionHistory.Last());
            NextTravelStepIndex--;
        }

        public void OnResume()
        {
            LoadItemsCommand.Execute(null);
        }

        public void ActionDeleteAllData()
        {
            view.ShowDeleteAllDataWarningPopupMessage();
        }

        public void ActionImportDataFromExcel()
        {
            view.ShowImportDataFromExcelPopUp();
        }

        public async Task DeleteAllData()
        {
            await cityService.DeleteAllAsync();
            LoadItemsCommand.Execute(null);
            view.NotifyListViewDataChanged();

            Clear();
        }

        public async Task ImportData()
        {
            if (Items.Count() != 0)
            {
                await cityService.DeleteAllAsync();
            }
            await ImportDataFromExcel();
            LoadItemsCommand.Execute(null);
            view.NotifyListViewDataChanged();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Items = Enumerable.Empty<CityDto>();
            SelectionHistory = new Collection<int>();
            TravelTotalDistance = 0;
            NextTravelStepIndex = START_TRAVEL_STEP_INDEX;

            LoadItemsCommand = new Command(async () => await LoadItems());
        }

        private async Task LoadItems()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items = await cityService.FindAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ImportDataFromExcel()
        {
            var items = await ReadDataFromExcel();
            if (items.Any())
            {
                await cityService.InsertItemsAsync(items);
            }
            else
            {
                view.ShowShortToastMessage("No data to be loaded");
            }
        }

        private async Task<IEnumerable<CityDto>> ReadDataFromExcel()
        {
            var path = view.GetFilePath(FILE_NAME);

            if (File.Exists(path))
            {
                return await Task.FromResult(ExcelHelper.ReadExcel(path));
            }
            else
            {
                view.ShowShortToastMessage("File <travelData.ods> not exists in the Download folder");
            }
            return Enumerable.Empty<CityDto>();
        }

        private void Clear()
        {
            SelectionHistory.Clear();
            TravelTotalDistance = 0;
            NextTravelStepIndex = START_TRAVEL_STEP_INDEX;
            foreach (var i in Items)
            {
                i.TravelSteps.Clear();
            }
        }

        private float GetDistanceFromTo(int sourceCityIndex, int targetCityIndex)
        {
            var sourceCity = Items.ElementAt(sourceCityIndex);
            var targetCity = Items.ElementAt(targetCityIndex);

            var distance = sourceCity.GetDistanceTo(targetCity.Name);

            if (distance == 0)
            {
                view.ShowShortToastMessage($"Source city <{sourceCity.Name}> has no reference to the target city <{targetCity.Name}>");
            }
            return distance;
        }

        #endregion
    }
}
