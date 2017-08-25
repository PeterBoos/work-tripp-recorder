using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace work_tripp_recorder
{
    class ListReportsAdapter : BaseAdapter<Item>
    {
        List<Item> items;
        Activity context;

        public ListReportsAdapter(Activity context, List<Item> items) : base()
        {
            this.context = context;
            this.items = items;
        }


        public override Item this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ItemRowView, null);
            }
            convertView.FindViewById<TextView>(Resource.Id.txtName).Text = item.ReadableTitle;
            //convertView.FindViewById<TextView>(Resource.Id.txtDate).Text = item.Date.ToString("yyyy-MM-dd hh:mm");
            convertView.FindViewById<TextView>(Resource.Id.txtDate).Text = item.Date.ToShortDateString();
            convertView.FindViewById<TextView>(Resource.Id.txtTotalMileage).Text = 
                item.MileageEnd == 0 ? "-" : (item.MileageEnd - item.MileageStart).ToString();
            convertView.FindViewById<TextView>(Resource.Id.txtVisited).Text = item.VisitedEntity;

            return convertView;
        }

        public void UpdateList(List<Item> items)
        {
            this.items = items;
            NotifyDataSetChanged();
        }
    }
}