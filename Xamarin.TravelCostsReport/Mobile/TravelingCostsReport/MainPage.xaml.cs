using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinnesLogic.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Core.Presenters;
using Core;
using Core.IViews;

namespace TravelingCostsReport
{
    public partial class MainPage : ContentPage, IViewTest
    {
        private readonly ViewTestPresenter presenter;

        public MainPage()
        {
            InitializeComponent();

            presenter = new ViewTestPresenter(this, (ITestService)Startup.ServiceProvider.GetService(typeof(ITestService)));
        }

        public IEnumerable<int> Test()
        {
            var test = presenter.GetItems();
            return test;
        }

        public int GetOneValue()
        {
            throw new NotImplementedException();
        }

        public void PressOkBtn()
        {
            throw new NotImplementedException();
        }
    }
}
