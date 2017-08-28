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
    [Activity(Label = "ReportEditActivity")]
    public class ReportEditActivity : Activity
    {
        private static ItemRepositoryNotAsync database;

        private Item item;

        private EditText txtDate;
        private EditText txtFromCity;
        private EditText txtToCity;
        private EditText txtVisited;
        private EditText txtPurpose;
        private EditText txtMileageStart;
        private EditText txtMileageEnd;
        private Button btnSave;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ReportEdit);

            database = new ItemRepositoryNotAsync();

            var itemId = Intent.GetIntExtra("ItemId", 0);
            if (itemId == 0)
            {
                // TODO: Handle error
            }
            item = database.GetItem(itemId);
            if (item == null)
            {
                // TODO: Handle error
            }

            LoadViewItems();
            LoadValues();

            btnSave.Click += BtnSave_Click;
        }

        private void LoadViewItems()
        {
            txtDate = FindViewById<EditText>(Resource.Id.txtEditDate);
            txtFromCity = FindViewById<EditText>(Resource.Id.txtEditFromCity);
            txtToCity = FindViewById<EditText>(Resource.Id.txtEditToCity);
            txtVisited = FindViewById<EditText>(Resource.Id.txtEditVisited);
            txtPurpose = FindViewById<EditText>(Resource.Id.txtEditPurpose);
            txtMileageStart = FindViewById<EditText>(Resource.Id.txtEditMileageStart);
            txtMileageEnd = FindViewById<EditText>(Resource.Id.txtEditMileageEnd);
            btnSave = FindViewById<Button>(Resource.Id.btnEditSave);
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
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            item.CityStart = txtFromCity.Text;
            item.CityEnd = txtToCity.Text;
            item.VisitedEntity = txtVisited.Text;
            item.Purpose = txtPurpose.Text;
            
            int mileageStart, mileageEnd;
            if (int.TryParse(txtMileageStart.Text, out mileageStart))
            {
                item.MileageStart = mileageStart;
            }
            if (int.TryParse(txtMileageEnd.Text, out mileageEnd))
            {
                item.MileageEnd = mileageEnd;
            }

            database.UpdateItem(item);
        }
    }
}