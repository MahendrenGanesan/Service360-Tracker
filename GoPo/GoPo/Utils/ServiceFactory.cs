using System;
using System.Collections.Generic;
using System.Text;
using GoPo.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoPo.Utils
{
   public class ServiceFactory
    {
        public static ServiceFactory Instance { get { return new ServiceFactory(); } }
        public static int _lock = 0;
        public int GetUserID(string userEmails)
        {
            foreach (string email in userEmails.Split(','))
            {
                User user = null;
                user = GetUserByEmail(email);
                if (user != null && user.uid > 0) return user.uid; ;
            }
            return -1;
        }
        public void SetGioPosition(int uid,double latitude, double longitude)
        {
            if (_lock != 0) return;
            _lock = 1;
            GoPo.Models.webApi.GioPosition gioPo = new GoPo.Models.webApi.GioPosition();
            gioPo.latitude = latitude;
            gioPo.longitude = longitude;
            gioPo.recoDt = DateTime.Now;
            gioPo.sid = 1;
            gioPo.uid = uid;
            var httpWebResp = HttpWebRequestHelper.WebPost(GoPo.Constants.URL_Post_GioPosition, gioPo);
            _lock = 0;
        }

        private  User GetUserByEmail(string email)
        {
            string userURL = string.Format(GoPo.Constants.URL_Get_UserByMail, email);
            User userObj=null;
                      
            var customWebResponse = HttpWebRequestHelper.WebGet(userURL);

            if (customWebResponse != null)
            {
                if (customWebResponse.Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    userObj = JsonConvert.DeserializeObject<User>(customWebResponse.ResponseData);
                }
            }
            return userObj;
        }
    }
}
