using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GioPo.Droid
{
	[Activity (Label = "GioPo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private Button registrationButton;
        private Button signInButton;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            FindViews();

            registrationButton.Click += Registration_Click;
            signInButton.Click += SignIn_Click;

            StartActivity(typeof(HomeActivity));
        }

        private void FindViews()
        {
            registrationButton = FindViewById<Button>(Resource.Id.buttonRegister);
            signInButton = FindViewById<Button>(Resource.Id.buttonSignIn);
        }

        private void Registration_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegistrationActivity));
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

    }
}


