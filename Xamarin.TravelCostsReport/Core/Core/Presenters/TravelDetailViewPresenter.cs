using System;
using System.Collections.Generic;
using BusinnesLogic.Services;
using BusinnesLogic.Dto;
using Core.Helpers;

namespace Core.Presenters
{
    public class TravelDetailViewPresenter : IDisposable
    {
        #region Private Fields

        private bool _disposed = false;
        private readonly ICityService cityService;

        #endregion

        #region Public Properties


        public IEnumerable<CityDto> Items { get; private set; }

        #endregion

        #region Commands

        public Command LoadItemsCommand { get; private set; }

        #endregion

        #region Constructors

        public TravelDetailViewPresenter(ICityService cityService)
        {
            this.cityService = cityService;
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

        #endregion
    }
}
