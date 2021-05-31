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

namespace Core.Presenters
{
    public class TravelDetailViewPresenter : BaseViewPresenter, IDisposable
    {
        #region Private Fields

        private bool _disposed = false;
        private readonly ICityService cityService;
        private readonly ITravelDetailView view;

        private CityDto lastCity;

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
            }

            _disposed = true;
        }

        public void AddDistanceToTotalTravelDistance(int position)
        {
            var city = Items.ElementAt(position);
            if (lastCity.IsEmpty())
            {
                TravelTotalDistance = 0;
                lastCity = city;
            }
            else
            {
                var tmp = lastCity.CityItems.FirstOrDefault(x => x.Name.ToLower().Equals(city.Name.ToLower()));
                TravelTotalDistance += tmp != null ? tmp.Distance : 0;
                lastCity = city;
            }

            Items.ElementAt(position).Index = string.IsNullOrEmpty(Items.ElementAt(position).Index)
                ? CityIndex.ToString()
                : string.Concat(Items.ElementAt(position).Index, ", ", CityIndex);

            CityIndex++;
        }

        public void SubtracktDistanceFromTotalTravelDistance(int position)
        {
            var city = Items.ElementAt(position);

            if (!lastCity.IsEmpty() && city.Equals(lastCity))
            {

            }
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
            TravelTotalDistance = 0;
            CityIndex = 1;

            LoadItemsCommand = new Command(async () => await LoadItems());

            lastCity = new CityDto();
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

        #endregion
    }
}
