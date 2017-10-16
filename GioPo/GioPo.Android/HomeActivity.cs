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

namespace GioPo.Droid
{
    [Activity(Label = "Home")]
    public class HomeActivity : Activity
    {
        private Button myTracksButton;
        private Button searchTracksButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Home);

            FindViews();

            myTracksButton.Click += MyTracksButton_Click;
            searchTracksButton.Click += SearchTracksButton_Click;
        }

        private void FindViews()
        {
            myTracksButton = FindViewById<Button>(Resource.Id.buttonMyTracks);
            searchTracksButton = FindViewById<Button>(Resource.Id.buttonSearchTrack);
        }

        private void MyTracksButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MyTracksActivity));
        }

        private void SearchTracksButton_Click(object sender, EventArgs e)
        {
            
        }

        public override void OnBackPressed()
        {
           
        }
    }
}