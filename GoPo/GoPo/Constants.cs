using System;
using System.Collections.Generic;
using System.Text;

namespace GoPo
{
    public class Constants
    {
        public static string GetCurrentPositionAPI = "http://tracker.service360.in/api/gioPosition/{0}";
        public static string URL_Post_GioPosition = "http://tracker.service360.in/api/gioPosition";
        public static string URL_Get_UserByID = "http://tracker.service360.in/api/user/GetById/1";
        public static string URL_Get_UserByMail = "http://tracker.service360.in/api/user/GetByMail/{0}/";
    }
}
