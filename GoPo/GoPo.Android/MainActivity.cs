using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Collections.Generic;
using GoPo.Utils;
using Android.Accounts;

namespace GoPo.Droid
{
	[Activity (Label = "GoPo.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener
    {
		int count = 1;
        int _uid = -1;
        private Location _currentGPSLocation;
        private LocationManager _locationManager;
        private AccountManager _accountManager;
        private string _locationProvider;
        private TextView _locationAddressText;
        private String _loginAccountEmails;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            InitializeAccountManager();
            InitializeLocationManager();
            // Get our button from the layout resource,
            // and attach an event to it
            Button btnEnableDisableTracking = FindViewById<Button> (Resource.Id.btnEnableDisableTracking);

            btnEnableDisableTracking.Click += delegate {
                if (btnEnableDisableTracking.Text.Equals("Enable"))
                {
                    btnEnableDisableTracking.Text="Disable";
                }
                else
                {
                    btnEnableDisableTracking.Text = "Enable";
                }

            };
		}

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            _locationProvider = _locationManager.GetBestProvider(criteriaForLocationService, false);
        }
        void InitializeAccountManager()
        {
            string temp = string.Empty;
            _accountManager = (AccountManager)GetSystemService(AccountService);
            Java.Util.Regex.Pattern emailPattern = Android.Util.Patterns.EmailAddress;
            Account[] accounts = _accountManager.GetAccounts();
            
            foreach (Account account in accounts)
            {
                if (emailPattern.Matcher(account.Name).Matches())
                {
                    String possibleEmail = account.Name;
                    if (temp.Length > 0)
                        temp = temp + ",";
                    temp = temp + possibleEmail;
                }
            }
            _loginAccountEmails = temp;
            _uid=ServiceFactory.Instance.GetUserID(_loginAccountEmails);
        }
        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider,5000,5, this);
            
        }
        public void OnLocationChanged(Location location)    
        {
            _currentGPSLocation = location;
            if (_currentGPSLocation != null)
            {
                ServiceFactory.Instance.SetGioPosition(_uid, _currentGPSLocation.Latitude, _currentGPSLocation.Longitude);
            }
           
        }
        private void SetGioPosition(Location location)
        {
            //GoPo.Models.webApi.GioPosition gioPo = new GoPo.Models.webApi.GioPosition();
            //gioPo.latitude = location.Latitude;
            //gioPo.longitude = location.Longitude;
            //gioPo.recoDt = DateTime.Now;
            //gioPo.sid = 1;
            //gioPo.uid = 1;


            //ServiceFactory.Instance.SetGioPosition(location.Latitude, location.Longitude);
          //var httpWebResp=  HttpWebRequestHelper.HttpWebResponse(GoPo.Constants.URL_Post_GioPosition, gioPo);

        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}


