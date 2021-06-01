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
    public class TravelDetailViewPresenter : BaseViewPresenter, IDisposable
    {
        #region Private Fields

        private bool _disposed = false;
        private readonly ICityService cityService;
        private readonly ITravelDetailView view;

        private ICollection<int> selectionHistory;

        #endregion

        #region Public Properties

        public IEnumerable<CityDto> Items { get; private set; }
        public float TravelTotalDistance { get; private set; }
        public int TravelStepIndex { get; private set; }

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
        protected virtual void Dispose(bool disposing)
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
            if (!selectionHistory.Any())
            {
                Items.ElementAt(position).AddTravelStep(TravelStepIndex);
                TravelTotalDistance = 0;
            }
            else
            {
                Items.ElementAt(position).AddTravelStep(TravelStepIndex);
                TravelTotalDistance += GetDistanceFromTo(selectionHistory.Last(), position);
            }
            selectionHistory.Add(position);
            TravelStepIndex++;
        }

        public void RemoveTravelStep(int position)
        {
            if (!selectionHistory.Any() ||
                Items.ElementAt(position).TravelSteps.Count == 0 ||
                position != selectionHistory.Last())
            {
                return;
            }

            TravelTotalDistance -= GetDistanceFromTo(selectionHistory.Last(), position);
            Items.ElementAt(position).RemoveLastTravelStep();
            selectionHistory.Remove(selectionHistory.Last());
            TravelStepIndex--;
        }

        public void OnResume()
        {
            LoadItemsCommand.Execute(null);

            if (Items.Count() == 0)
            {
                ImportDataFromExcel()
                    .GetAwaiter()
                    .GetResult();

                LoadItemsCommand.Execute(null);
            }
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Items = Enumerable.Empty<CityDto>();
            selectionHistory = new Collection<int>();
            TravelTotalDistance = 0;
            TravelStepIndex = 1;

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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task ImportDataFromExcel()
        {
            var items = ReadDataFromExcel();
            if (items.Count() > 0)
            {
                cityService.InsertItemsAsync(items);
            }
            else
            {
                view.ShowErrorMessage("No data to be loaded");
            }
            return Task.CompletedTask;
        }

        private IEnumerable<CityDto> ReadDataFromExcel()
        {
            var fileName = "travelData.ods";
            var path = Path.Combine(
                Android.OS.Environment.ExternalStorageDirectory.Path,
                Android.OS.Environment.DirectoryDownloads,
                fileName);

            if (File.Exists(path))
            {
                return ExcelHelper.ReadExcel(path);
            }
            else
            {
                view.ShowErrorMessage("File <travelData.ods> not exists in the Download folder");
            }
            return Enumerable.Empty<CityDto>();
        }

        private void Clear()
        {
            selectionHistory.Clear();
            TravelTotalDistance = 0;
            TravelStepIndex = 1;
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
                view.ShowErrorMessage($"Source city <{sourceCity.Name}> has no reference to the target city <{targetCity.Name}>");
            }
            return distance;
        }

        #endregion
    }
}
