using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace work_tripp_recorder
{
    [Activity(Label = "ReportDetailsActivity")]
    public class ReportDetailsActivity : Activity
    {
        static ItemRepositoryNotAsync database;

        private Item item;

        private TextView txtDate;
        private TextView txtFromCity;
        private TextView txtToCity;
        private TextView txtVisited;
        private TextView txtPurpose;
        private TextView txtMileageStart;
        private TextView txtMileageEnd;
        private TextView txtDistance;
        private Button btnEdit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ReportDetails);

            database = new ItemRepositoryNotAsync();

            var itemId = Intent.GetIntExtra("ItemId", 0);
            if (itemId == 0)
            {
                // TODO: handle error
            }
            item = database.GetItem(itemId);

            if (item == null)
            {
                // TODO: Handle error
            }

            LoadViewItems();
            LoadValues();

            btnEdit.Click += BtnEdit_Click;
        }

        private void LoadViewItems()
        {
            txtDate = FindViewById<TextView>(Resource.Id.txtDetailDate);
            txtFromCity = FindViewById<TextView>(Resource.Id.txtDetailFromCity);
            txtToCity = FindViewById<TextView>(Resource.Id.txtDetailToCity);
            txtVisited = FindViewById<TextView>(Resource.Id.txtDetailVisited);
            txtPurpose = FindViewById<TextView>(Resource.Id.txtDetailPurpose);
            txtMileageStart = FindViewById<TextView>(Resource.Id.txtDetailMileageStart);
            txtMileageEnd = FindViewById<TextView>(Resource.Id.txtDetailMileageEnd);
            txtDistance = FindViewById<TextView>(Resource.Id.txtDetailDistance);
            btnEdit = FindViewById<Button>(Resource.Id.btnDetailEdit);
        }

        private void LoadValues()
        {
            txtDate.Text = item.Date.ToShortDateString();
            txtFromCity.Text = item.CityStart;
            txtToCity.Text = item.CityEnd;
            txtVisited.Text = item.VisitedEntity;
            txtPurpose.Text = item.Purpose;
            txtMileageStart.Text = item.MileageStart.ToString();
            txtMileageEnd.Text = item.MileageEnd.ToString();
            txtDistance.Text = (item.MileageStart != 0 && item.MileageEnd != 0)
                ? (item.MileageEnd - item.MileageStart).ToString()
                : "-";
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ReportDetailsActivity));
            intent.PutExtra("ItemId", item.Id);
            StartActivity(intent);
        }
    }
}