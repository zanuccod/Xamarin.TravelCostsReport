using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Core.IViews;
using Core.Presenters;
using BusinnesLogic.Services;
using Core;
using TravelingCostsReport.Droid.Adapters;
using AndroidX.RecyclerView.Widget;
using System.IO;
using Xamarin.Essentials;
using BusinnesLogic.Helpers;
using Android;
using Android.Content.PM;
using System.Threading.Tasks;

namespace TravelingCostsReport.Droid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ITravelDetailView
    {
        private TravelDetailViewPresenter presenter;
        private TravelDetailsItemAdapter adapterItem;

        private ItemTouchHelper mItemTouchHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            presenter = new TravelDetailViewPresenter(this, Startup.GetService<ICityService>());

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapterItem = new TravelDetailsItemAdapter(this, presenter));

            ItemTouchHelper.SimpleCallback simpleCallback = new Helpers.ItemTouchHelper(
                0,
                ItemTouchHelper.Left | ItemTouchHelper.Right,
                adapterItem);

            mItemTouchHelper = new ItemTouchHelper(simpleCallback);
            mItemTouchHelper.AttachToRecyclerView(recyclerView);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            GetExternalPermissions();
        }

        protected override void OnResume()
        {
            base.OnResume();

            presenter.OnResume();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            adapterItem.Dispose();
            presenter.Dispose();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_import_excel_data)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void ShowErrorMessage(string message)
        {
            Android.Widget.Toast.MakeText(this, message, Android.Widget.ToastLength.Short).Show();
        }

        public void GetExternalPermissions()
        {
            string[] PermissionsLocation =
            {
                Manifest.Permission.ReadExternalStorage
            };
            if (CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Permission.Granted)
            {
                return;
            }

            if (ShouldShowRequestPermissionRationale(Manifest.Permission.ReadExternalStorage))
            {
                ShowErrorMessage("We need read permission to read the data from the excel file.");
            }
            RequestPermissions(PermissionsLocation, 999);
        }
    }
}
