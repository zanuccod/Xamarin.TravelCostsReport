using Android.Graphics;
using AndroidX.RecyclerView.Widget;

namespace TravelingCostsReport.Droid.Helpers
{
    public class ItemTouchHelper : AndroidX.RecyclerView.Widget.ItemTouchHelper.SimpleCallback
    {
        private IItemTouchHelperListener listener;
        private RecyclerView recyclerView;

        public ItemTouchHelper(int dragDirs, int swipeDirs, IItemTouchHelperListener listener)
            :base(dragDirs, swipeDirs)
        {
            this.listener = listener;
        }

        public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
        {
            return true;
        }

        public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
        {
            listener.onSwiped(p0, p1, 0);
            recyclerView.GetAdapter().NotifyItemChanged(p0.AdapterPosition);
        }

        public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
        {
            this.recyclerView = recyclerView;
            base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);
        }
    }
}
