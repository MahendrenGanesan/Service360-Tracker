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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using System.Threading.Tasks;
using GioPo.ViewModels;
using System.Threading;

namespace GioPo.Droid
{
    [Activity(Label = "TrackerActivity")]
    public class TrackerActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        #region Fields
        private LatLng _currentLocation;
        private LatLng _lastLocation;
        private GoogleMap _map;
        private MapFragment _mapFragment;
        private Button _btnGoToLocation;
        private Timer _apiTimer;
        private TrackerMapViewModel _trackerMapViewModel;
        private bool _apiCallInProgress = false;

        //GPS Location
        private Location _currentGPSLocation;
        private LocationManager _locationManager;
        private string _locationProvider;
        private TextView _locationAddressText;
        private bool _isBeginNavtoCurLocation = false;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Tracker);

            ActionBar.Hide();

            //Initialize
            _locationAddressText = FindViewById<TextView>(Resource.Id.address_text);
            _btnGoToLocation = FindViewById<Button>(Resource.Id.btnTrack);
            _btnGoToLocation.Click += btnGoToLocation_Click;
            _btnGoToLocation.Visibility = ViewStates.Gone;
            _locationAddressText.Visibility = ViewStates.Gone;

            //Timer to get API GEO Location
            TimerCallback timerDelegate = new TimerCallback(OnAPILocationChange);
            _apiTimer = new Timer(timerDelegate, null, 1000, 10000);

            _trackerMapViewModel = new TrackerMapViewModel();

            InitMapFragment();

            //FOR Current Device GPS Track
            InitializeLocationManager();
        }

        protected override void OnResume()
        {
            base.OnResume();

            //Device GPS Location, If Not Comment Out
            _locationManager.RequestLocationUpdates(_locationProvider, 5000, 5, this);

            SetMyCurrentMapLocation();
        }

        async void btnGoToLocation_Click(object sender, EventArgs e)
        {
            NavigateToMyCurrentLocation();
        }


        async void OnAPILocationChange(Object state)
        {
            if (!_apiCallInProgress)
            {
                _apiCallInProgress = true;
                var latestTracks = await _trackerMapViewModel.GetLatestTrackPosition("3");
                if (latestTracks.Any())
                {
                    _currentLocation = new LatLng(latestTracks.First().Latitude, latestTracks.First().Longitude);
                    if (_lastLocation == null || _lastLocation.Latitude != _currentLocation.Latitude)
                    {
                        this.RunOnUiThread(() =>
                        {
                            SetMyCurrentMapLocation();
                            if (!_isBeginNavtoCurLocation)
                            {
                                NavigateToMyCurrentLocation();
                                _isBeginNavtoCurLocation = true;
                            }
                        });

                        _lastLocation = _currentLocation;
                    }
                }
                _apiCallInProgress = false;
            }
        }

        #region Map Methods
        private void InitMapFragment()
        {
            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;
            if (_mapFragment == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeCompassEnabled(true)
                    //.InvokeRotateGesturesEnabled(true)
                    //.InvokeTiltGesturesEnabled(true)
                    //.InvokeMapToolbarEnabled(true)
                    //.InvokeZOrderOnTop(true)
                    //.InvokeZoomGesturesEnabled(true)
                    //.InvokeLiteMode(true)
                    //.InvokeAmbientEnabled(true)
                    .InvokeMapToolbarEnabled(true)
                    .InvokeZoomControlsEnabled(false);


                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }
            _mapFragment.GetMapAsync(this);

        }

        private void SetMyCurrentMapLocation()
        {
            if (_map != null && _currentLocation != null)
            {
                _map.Clear();
                MarkerOptions markerOpt1 = new MarkerOptions();
                markerOpt1.SetPosition(_currentLocation);
                markerOpt1.SetTitle("My Target Location");
                BitmapDescriptor icon = BitmapDescriptorFactory.FromAsset("Spot.png");
                markerOpt1.SetIcon(icon);
                _map.AddMarker(markerOpt1);

                //MarkerOptions markerOpt2 = new MarkerOptions();
                //markerOpt2.SetPosition(_currentLatLon);
                //markerOpt2.SetTitle("Passchendaele");
                //_map.AddMarker(markerOpt2);

                // We create an instance of CameraUpdate, and move the map to it.
                //CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLng(_currentLatLon);
                //_map.MoveCamera(cameraUpdate);

            }
        }

        public void OnMapReady(GoogleMap map)
        {
            _map = map;

            //_map.UiSettings.MapToolbarEnabled = true;
            
            if (_currentLocation != null)
            {
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(_currentLocation, 15);
                _map.MoveCamera(cameraUpdate);

                //SetMyCurrentMapLocation();
            }
        }

        async void NavigateToMyCurrentLocation()
        {
            if (_currentLocation != null)
            {
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(_currentLocation, 17);
                _map.MoveCamera(cameraUpdate);

                Address address = await ReverseGeocodeCurrentLocation();
                DisplayAddress(address);
            }

        }

        #endregion

        #region Location Listener
        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
        }

        public void OnLocationChanged(Location location)
        {
            _currentGPSLocation = location;
            if (_currentGPSLocation != null)
            {
                this.PostGPSDataToApi();//Save GPS data to API
                /* //Populate my GPS to map
                _currentLocation = new LatLng(location.Latitude, location.Longitude);
                if (_lastLocation == null || _lastLocation.Latitude != _currentLocation.Latitude)
                {
                    SetMyCurrentMapLocation();
                    if (!_isBeginNavtoCurLocation)
                    {
                        NavigateToMyCurrentLocation();
                        _isBeginNavtoCurLocation = true;
                    }
                    _lastLocation = _currentLocation;
                }
                */

            }
        }

        public void OnProviderDisabled(string provider)
        { }

        public void OnProviderEnabled(string provider)
        { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        { }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }

        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i <= address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                _locationAddressText.Text = deviceAddress.ToString();
            }
            else
            {
                _locationAddressText.Text = "Unable to determine the address. Try again in a few minutes.";
            }
        }

        async void PostGPSDataToApi()
        {
            if (!_isGPSPostInProgress)
            {
                _isGPSPostInProgress = true;
                var lasttrack = await _trackerMapViewModel.PostGPSPosition(_currentGPSLocation.Latitude, _currentGPSLocation.Longitude, 3);
                _isGPSPostInProgress = false;
            }
        }
        private bool _isGPSPostInProgress = false;
        #endregion
    }
}