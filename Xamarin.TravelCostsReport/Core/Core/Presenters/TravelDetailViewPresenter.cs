using System;
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
        public int CityIndex { get; private set; }

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

        public void AddDistanceToTotalTravelDistance(int position)
        {
            if (!selectionHistory.Any())
            {
                Items.ElementAt(position).Index = CityIndex.ToString();
                TravelTotalDistance = 0;
            }
            else
            {
                Items.ElementAt(position).Index = string.IsNullOrEmpty(Items.ElementAt(position).Index)
                ? CityIndex.ToString()
                : string.Concat(Items.ElementAt(position).Index, ", ", CityIndex);

                var previousCity = Items.ElementAt(selectionHistory.Last());
                var selectedCity = Items.ElementAt(position);
                TravelTotalDistance += previousCity
                    .CityItems.FirstOrDefault(x => x.Name.ToLower().Equals(selectedCity.Name.ToLower()))
                    .Distance;
            }
            selectionHistory.Add(position);
            CityIndex++;

            //var city = Items.ElementAt(position);
            //if (lastCity.IsEmpty())
            //{
            //    TravelTotalDistance = 0;
            //    lastCity = city;
            //}
            //else
            //{
            //    var tmp = lastCity.CityItems.FirstOrDefault(x => x.Name.ToLower().Equals(city.Name.ToLower()));
            //    TravelTotalDistance += tmp != null ? tmp.Distance : 0;
            //    lastCity = city;
            //}

            //Items.ElementAt(position).Index = string.IsNullOrEmpty(Items.ElementAt(position).Index)
            //    ? CityIndex.ToString()
            //    : string.Concat(Items.ElementAt(position).Index, ", ", CityIndex);

            //CityIndex++;
        }

        public void SubtracktDistanceFromTotalTravelDistance(int position)
        {
            if (!selectionHistory.Any() ||
                string.IsNullOrEmpty(Items.ElementAt(position).Index) ||
                position != selectionHistory.Last())
            {
                return;
            }
            else if (selectionHistory.Count == 1)
            {
                Items.ElementAt(position).Index = string.Empty;
            }
            else
            {
                var previousCity = Items.ElementAt(selectionHistory.Last());
                var selectedCity = Items.ElementAt(position);

                // this because "S,Canzian" have different names on the excel
                var previousCityDistance = previousCity
                        .CityItems.FirstOrDefault(x => x.Name.ToLower().Equals(selectedCity.Name.ToLower()));
                TravelTotalDistance -= previousCityDistance != null
                    ? previousCityDistance.Distance
                    : 0;

                var tmp = Items.ElementAt(position).Index.Split(",");

                var items = tmp.Take(tmp.Count() - 1);
                Items.ElementAt(position).Index = string.Join(", ", items);
            }

            selectionHistory.Remove(selectionHistory.Last());
            CityIndex--;
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
            CityIndex = 1;

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
            CityIndex = 1;
            foreach (var i in Items)
            {
                i.Index = string.Empty;
            }
        }

        #endregion
    }
}
