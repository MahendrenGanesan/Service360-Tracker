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
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        private Button signInButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            this.ActionBar.SetHomeButtonEnabled(true);
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);

            FindViews();

            signInButton.Click += SignIn_Click;
        }

        private void FindViews()
        {
            signInButton = FindViewById<Button>(Resource.Id.buttonSignIn);
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HomeActivity));
        }
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);

            //base.OnBackPressed(); -> DO NOT CALL THIS LINE OR WILL NAVIGATE BACK
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}