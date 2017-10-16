using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.DAL;
using System.Data;
namespace Tracker.BLL
{
    public class GioPositionBLL
    {
        public static GioPositionBLL Instance { get { return new GioPositionBLL(); } }

        public List<Tracker.Entity.web.GioPosition> GetGioPosition(int topItems, int uid)
        {
            DBConnect db = new DBConnect();
            DataSet ds = db.Select(string.Format(@"Select * from tblgioposition gp where gp.uid ={1} and recoDt > DATE_SUB(NOW(), INTERVAL 2 HOUR) order by recoDt desc limit {0} ", topItems, uid));
            List<Tracker.Entity.web.GioPosition> gioPositions = new List<Entity.web.GioPosition>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                    gioPositions.Add(new Entity.web.GioPosition
                    {
                        gpID = int.Parse(dr["gpid"].ToString()),
                        sid = int.Parse(dr["sid"].ToString()),
                        uid = int.Parse(dr["uid"].ToString()),
                        longitude = float.Parse(dr["longitude"].ToString()),
                        latitude = float.Parse(dr["latitude"].ToString()),
                        recoDt = DateTime.Parse(dr["recoDt"].ToString())
                    });
            return gioPositions;
        }
        //public Tracker.Entity.web.GioPosition GetGioPosition(int topItems,string email)
        //{
        //    DBConnect db = new DBConnect();
        //    DataSet ds = db.Select(string.Format(@"Select top {0} * from tblgioposition gp
        //    inner join tblUser usr on gp.uid = usr.uid and recoDt > DATE_SUB(NOW(), INTERVAL 2 HOUR) AND usr.email='{1}'",topItems,email));
        //    Tracker.Entity.web.GioPosition gioPosition = null;
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        gioPosition = new Entity.web.GioPosition
        //        {
        //            gpID = int.Parse(ds.Tables[0].Rows[0]["gpid"].ToString()),
        //            sid = int.Parse(ds.Tables[0].Rows[0]["sid"].ToString()),
        //            uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString()),
        //            longitude= float.Parse(ds.Tables[0].Rows[0]["longitude"].ToString()),
        //            latitude= float.Parse(ds.Tables[0].Rows[0]["latitude"].ToString()),
        //            recoDt = DateTime.Parse( ds.Tables[0].Rows[0]["recoDt"].ToString())
        //        };
        //    return gioPosition;
        //}

        public int SetGioPosition(Tracker.Entity.web.GioPosition GioPosition)
        {
            if (GioPosition != null)
            {
                DBConnect db = new DBConnect();
                string insert = string.Format("Insert into tblgioposition (uid,sid,longitude,latitude,recoDt) values('{0}','{1}','{2}','{3}',now());", GioPosition.uid, GioPosition.sid, GioPosition.longitude, GioPosition.latitude);
                return db.Insert(insert);
            }
            return -1;
        }
    }
}
