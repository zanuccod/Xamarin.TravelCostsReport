using System;
using BusinnesLogic.Repository;
using BusinnesLogic.Services;
using Core.Presenters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TravelingCostsReport
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Core.Startup.Init();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
