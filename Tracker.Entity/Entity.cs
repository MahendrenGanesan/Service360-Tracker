using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Entity.web
{
    public class User
    {
        public int uid;
        public string Phone;
        public string Email;
        public string details;
        public DateTime suspDt;
    }
    public class Service
    {
        public int sid;
        public int uid;
        public string sType;
        public string sPhone;
        public string srcDtil;
        public string dtil;
        public DateTime statDt;
        public DateTime expDt;
        public DateTime suspDt;
    }
    public class GioPosition
    {
        public int gpID;
        public int sid;
        public int uid;
        public float longitude;
        public float latitude;
        public DateTime recoDt;
    }
}
