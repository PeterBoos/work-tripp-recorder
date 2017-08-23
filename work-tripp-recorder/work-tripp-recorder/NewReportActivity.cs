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
    [Activity(Label = "NewReportActivity")]
    public class NewReportActivity : Activity
    {
        private static ItemRepositoryNotAsync database;

        EditText txtDate;
        EditText txtFromCity;
        EditText txtToCity;
        EditText txtCompanyPerson;
        EditText txtPurpose;
        EditText txtMileageStart;
        EditText txtMileageEnd;

        Button btnSaveReport;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewReport);

            database = new ItemRepositoryNotAsync();

            LoadViewItems();

            btnSaveReport.Click += BtnSaveReport_Click;
        }

        void LoadViewItems()
        {
            txtDate = FindViewById<EditText>(Resource.Id.txtDate);
            txtFromCity = FindViewById<EditText>(Resource.Id.txtFromCity);
            txtToCity = FindViewById<EditText>(Resource.Id.txtToCity);
            txtCompanyPerson = FindViewById<EditText>(Resource.Id.txtCompanyPerson);
            txtPurpose = FindViewById<EditText>(Resource.Id.txtPurpose);
            txtMileageStart = FindViewById<EditText>(Resource.Id.txtMileageStart);
            txtMileageEnd = FindViewById<EditText>(Resource.Id.txtMileageEnd);

            btnSaveReport = FindViewById<Button>(Resource.Id.btnSaveReport);
        }

        private void BtnSaveReport_Click(object sender, System.EventArgs e)
        {
            var item = new Item
            {
                Date = Convert.ToDateTime(txtDate.Text),
                CityStart = txtFromCity.Text,
                CityEnd = txtToCity.Text,
                VisitedEntity = txtCompanyPerson.Text,
                Purpose = txtPurpose.Text
            };

            int mileageStart, mileageEnd;
            if (int.TryParse(txtMileageStart.Text, out mileageStart))
            {
                item.MileageStart = mileageStart;
            }
            if (int.TryParse(txtMileageEnd.Text, out mileageEnd))
            {
                item.MileageEnd = mileageEnd;
            }

            try
            {
                database.AddItem(item);
                Toast.MakeText(this, $"Rapport sparad.", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Kunde inte spara rapport =(.", ToastLength.Long).Show();
                var p = ex;
            }
        }
    }
}