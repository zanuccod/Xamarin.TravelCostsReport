using System;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BusinnesLogic.Dto;
using Core.Presenters;

namespace TravelingCostsReport.Droid.Adapters
{
    public class ItemsViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView OrderList { get; private set; }

        public ItemsViewHolder(View itemView, Action<int> onClickListener, Action<int> onLongClickListener)
            : base(itemView)
        {
            // Locate and cache view references:
            Name = itemView.FindViewById<TextView>(Resource.Id.adapter_item_city_name);
            OrderList = itemView.FindViewById<TextView>(Resource.Id.adapter_item_order_list);

            itemView.Click += (sender, e) => onClickListener(AdapterPosition);
            itemView.LongClick += (sender, e) => onLongClickListener(AdapterPosition);
        }
    }

    public class TravelDetailsItemAdapter : RecyclerView.Adapter
    {
        public event EventHandler<CityDto> ItemClick;
        public event EventHandler<CityDto> ItemLongClick;

        private readonly Activity activity;
        private readonly TravelDetailViewPresenter viewModel;


        public TravelDetailsItemAdapter(Activity activity, TravelDetailViewPresenter viewModel)
        {
            this.activity = activity;
            this.viewModel = viewModel;
        }

        private void OnClick(int position) => ItemClick?.Invoke(this, viewModel.Items.ElementAt(position));
        private void OnLongClick(int position) => ItemLongClick?.Invoke(this, viewModel.Items.ElementAt(position));

        public override int ItemCount => viewModel.Items.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ItemsViewHolder vh = holder as ItemsViewHolder;

            // Load the photo caption from the photo album:
            vh.Name.Text = viewModel.Items.ElementAt(position).Name;
            vh.OrderList.Text = string.Empty;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                                          Inflate(Resource.Layout.adapter_travel_item, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            ItemsViewHolder item = new ItemsViewHolder(itemView, OnClick, OnLongClick);
            return item;
        }
    }
}
