using GioPo.Models;
using GioPo.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GioPo.ViewModels
{
    public class TrackerMapViewModel
    {
        public TrackerMapViewModel()
        {

        }

        public async Task<List<TrackPosition>> GetLatestTrackPosition(string TrackingNumber)
        {
            List<TrackPosition> latestPositions = new List<TrackPosition>();
            var customWebResponse = await HttpWebRequestHelper.GetHttpWebResponse(string.Format(Constants.GetCurrentPositionAPI, TrackingNumber));

            if (customWebResponse != null)
            {
                if (customWebResponse.Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    latestPositions = this.ConvertResponseToTrackPosition(customWebResponse.ResponseData);
                }
                else//Test Data
                {
                    latestPositions = this.ConvertResponseToTrackPosition("");
                }
            }

            return latestPositions;
        }

        public async Task<bool> PostGPSPosition(double latitude, double longitude, int trackId)
        {
            TrackPosition latestPosition = new TrackPosition();
            var postBody = new { sid = 1, uid = trackId, longitude = longitude, latitude = latitude, recoDt = DateTime.Now };
            var customWebResponse = await HttpWebRequestHelper.PostHttpWebResponse(Constants.PostCurrentPositionAPI, postBody);

            if (customWebResponse != null)
            {
                if (customWebResponse.Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // latestPosition = this.ConvertResponseToTrackPosition(customWebResponse.ResponseData);
                }
            }

            return true;
        }

        private List<TrackPosition> ConvertResponseToTrackPosition(string response)
        {
            List<TrackPosition> latestPositions = new List<TrackPosition>();

            if (!string.IsNullOrEmpty(response))
            {
                var jsonResponse = JArray.Parse(response);

                if (jsonResponse.Any())
                {
                    foreach (var item in jsonResponse)
                    {
                        var latestPosition = new TrackPosition();
                        latestPosition.Longitude = item["longitude"].Value<double>();
                        latestPosition.Latitude = item["latitude"].Value<double>();
                        latestPosition.DisplayName = "Test API Location";

                        latestPositions.Add(latestPosition);
                    }
                }
            }
            else//Test
            {
                var lon = 76.31947642;
                var lat = 9.98954503;
                for (int i = 0; i < 5; i++)
                {
                    var latestPosition = new TrackPosition();
                    latestPosition.Longitude = lon + (i * 0.100);
                    latestPosition.Latitude = lat + (i * 0.100); ;
                    latestPosition.DisplayName = "Test Location";
                    latestPositions.Add(latestPosition);
                }
                
            }
            return latestPositions;
        }
    }
}
