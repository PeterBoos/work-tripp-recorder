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
using work_tripp_recorder.Utilities;

namespace work_tripp_recorder
{
    [Activity(Label = "DayActivity")]
    public class DayActivity : Activity
    {
        private static ItemRepositoryNotAsync database;

        private TextView txtDate;
        private Button btnPickDate;

        private DateTime pickedDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Day);

            database = new ItemRepositoryNotAsync();

            LoadViews();

            btnPickDate.Click += BtnPickDate_Click;
        }

        private void LoadViews()
        {
            txtDate = FindViewById<TextView>(Resource.Id.txtDayDate);
            btnPickDate = FindViewById<Button>(Resource.Id.btnDayPickDate);
        }

        private void BtnPickDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtDate.Text = time.ToShortDateString();
                pickedDate = time.Date;
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);

            var item = FindItemOnDate(pickedDate);
            if (item == null)
            {
                Toast.MakeText(this, $"Inget eller flera journaler hittades under det valda datumet.", ToastLength.Long).Show();
            }
            else
            {
                var intent = new Intent(this, typeof(ReportDetailsActivity));
                intent.PutExtra("ItemId", item.Id);
                StartActivity(intent);
            }
        }

        private Item FindItemOnDate(DateTime date)
        {
            var items = database.GetItems().Where(i => i.Date == date).ToList();

            if (!items.Any())
            {
                return null;
            }

            if (items.Count > 1)
            {
                return null;
            }

            return items[0];
        }
    }
}