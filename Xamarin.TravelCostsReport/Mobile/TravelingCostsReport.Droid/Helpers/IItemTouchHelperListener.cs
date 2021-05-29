using AndroidX.RecyclerView.Widget;

namespace TravelingCostsReport.Droid.Helpers
{
    public interface IItemTouchHelperListener
    {
        void onSwiped(RecyclerView.ViewHolder viewHolder, int direction, int position);
    }
}
