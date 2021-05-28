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

        #endregion

        #region Public Properties

        public IEnumerable<CityDto> Items { get; private set; }
        public float TravelTotalDistance { get; private set; }

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

        public void AddDistanceToTotalTravelDistance(float distance)
        {
            TravelTotalDistance += distance;
        }

        public void SubtracktDistanceFromTotalTravelDistance(float distance)
        {
            TravelTotalDistance -= distance;
        }

        public void OnResume()
        {
            LoadItemsCommand.Execute(null);
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Items = Enumerable.Empty<CityDto>();
            TravelTotalDistance = 0;

            LoadItemsCommand = new Command(async () => await LoadItems());
        }

        private Task LoadItems()
        {

            if (IsBusy)
            {
                return Task.CompletedTask;
            }

            IsBusy = true;

            try
            {
                //Items = await cityService.FindAllAsync();
                Items = ImportDataFromExcel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
            return Task.CompletedTask;
        }

        public IEnumerable<CityDto> ImportDataFromExcel()
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
            return Enumerable.Empty<CityDto>();
        }

        #endregion
    }
}
