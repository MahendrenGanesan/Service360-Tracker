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
    [Activity(Label = "MyTracksActivity")]
    public class MyTracksActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application 
            SetContentView(Resource.Layout.MyTracks);

            var myTrack = FindViewById<Button>(Resource.Id.buttonMyTrack1);

            myTrack.Click += MyTrack_Click;
        }

        private void MyTrack_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(TrackerActivity));
            //StartActivity(typeof(BaseMapActivity));
        }
    }
}