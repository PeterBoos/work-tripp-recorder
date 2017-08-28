using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using work_tripp_recorder.Utilities;

namespace work_tripp_recorder
{
    [Activity(Label = "NewReportActivity")]
    public class NewReportActivity : Activity
    {
        private static ItemRepositoryNotAsync database;

        TextView txvSelectedDate;
        Button btnPickDate;
        EditText txtFromCity;
        EditText txtToCity;
        EditText txtCompanyPerson;
        EditText txtPurpose;
        EditText txtMileageStart;
        EditText txtMileageEnd;
        
        Button btnSaveReport;

        DateTime startDate;

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
            txvSelectedDate = FindViewById<TextView>(Resource.Id.txvSelectedDate);
            btnPickDate = FindViewById<Button>(Resource.Id.btnPickDate);
            txtFromCity = FindViewById<EditText>(Resource.Id.txtFromCity);
            txtToCity = FindViewById<EditText>(Resource.Id.txtToCity);
            txtCompanyPerson = FindViewById<EditText>(Resource.Id.txtCompanyPerson);
            txtPurpose = FindViewById<EditText>(Resource.Id.txtPurpose);
            txtMileageStart = FindViewById<EditText>(Resource.Id.txtMileageStart);
            txtMileageEnd = FindViewById<EditText>(Resource.Id.txtMileageEnd);

            btnSaveReport = FindViewById<Button>(Resource.Id.btnSaveReport);

            btnPickDate.Click += BtnPickDate_Click;
        }

        private void BtnPickDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txvSelectedDate.Text = time.ToShortDateString();
                startDate = time.Date;
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtnSaveReport_Click(object sender, System.EventArgs e)
        {
            var item = new Item
            {
                Date = startDate,
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

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Kunde inte spara rapport =(.", ToastLength.Long).Show();
                var p = ex;
            }
        }
    }
}