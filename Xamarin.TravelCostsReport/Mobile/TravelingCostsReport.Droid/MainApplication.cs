using System;
using Android.App;
using Serilog;

namespace TravelingCostsReport.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public static MainApplication Current { get; private set; }

        public MainApplication(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.AndroidLog()
                .MinimumLevel.Debug()
                .CreateLogger();
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Core.Startup.Init();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();

        }
    }
}
