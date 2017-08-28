using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace work_tripp_recorder
{
    [Activity(Label = "ListReportsActivty")]
    public class ListReportsActivty : Activity
    {
        private static ItemRepositoryNotAsync database;

        private List<Item> items;
        private ListView itemListView;

        private ListReportsAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListReports);

            database = new ItemRepositoryNotAsync();

            items = new List<Item>();
            GetItems();

            itemListView = FindViewById<ListView>(Resource.Id.itemListView);
            adapter = new ListReportsAdapter(this, items);
            itemListView.Adapter = adapter;
            itemListView.ItemClick += ItemListView_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            GetItems();
            adapter.UpdateList(items);
        }

        private void GetItems()
        {
            items = database.GetItems().OrderByDescending(i => i.Date).ToList();
        }

        private void ItemListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = items[e.Position];
            var intent = new Intent(this, typeof(ReportDetailsActivity));
            intent.PutExtra("ItemId", item.Id);
            StartActivity(intent);
        }
    }
}