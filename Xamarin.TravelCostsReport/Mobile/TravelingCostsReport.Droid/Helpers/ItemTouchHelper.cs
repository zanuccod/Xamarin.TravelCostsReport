using System;
using Android.Graphics;
using AndroidX.RecyclerView.Widget;

namespace TravelingCostsReport.Droid.Helpers
{
    public class ItemTouchHelper : AndroidX.RecyclerView.Widget.ItemTouchHelper.Callback
    {
        private IItemTouchHelperListener listener;

        public ItemTouchHelper(IItemTouchHelperListener listener)
        {
            this.listener = listener;
        }

        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            const int swipeFlags = AndroidX.RecyclerView.Widget.ItemTouchHelper.Left | AndroidX.RecyclerView.Widget.ItemTouchHelper.Right;
            const int dragFlags = AndroidX.RecyclerView.Widget.ItemTouchHelper.AnimationTypeSwipeSuccess;
            return MakeMovementFlags(dragFlags, swipeFlags);
        }

        public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
        {
            // Notify the adapter of the dismissal
            return true;
        }

        public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
        {
            // Notify the adapter of the dismissal
            listener.onSwiped(p0, p1, 0);
        }
    }
}
