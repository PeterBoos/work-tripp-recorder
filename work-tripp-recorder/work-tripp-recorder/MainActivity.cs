using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace work_tripp_recorder
{
    [Activity(Label = "work_tripp_recorder", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);

            var btnAddReport = FindViewById<Button>(Resource.Id.btnNewReport);
            var btnListReports = FindViewById<Button>(Resource.Id.btnListAllReports);
            var btnDay = FindViewById<Button>(Resource.Id.btnCompilationDay);

            btnAddReport.Click += BtnAddReport_Click;
            btnListReports.Click += BtnListReports_Click;
            btnDay.Click += BtnDay_Click;
        }

        private void BtnAddReport_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewReportActivity));
            StartActivity(intent);
        }

        private void BtnListReports_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ListReportsActivty));
            StartActivity(intent);
        }

        private void BtnDay_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(DayActivity));
            StartActivity(intent);
        }
    }
}

