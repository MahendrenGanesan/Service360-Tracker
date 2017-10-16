using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.DAL;
using System.Data;
namespace Tracker.BLL
{
    public class ServiceBLL
    {
        public static ServiceBLL Instance { get { return new ServiceBLL(); } }
        public Tracker.Entity.web.Service GetService(int id)
        {
            DBConnect db = new DBConnect();
            DataSet ds = db.Select("select * from tblService where sid=" + id.ToString());
            Tracker.Entity.web.Service service = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                service = new Entity.web.Service
                {
                    sid = int.Parse(ds.Tables[0].Rows[0]["sid"].ToString()),
                    uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString()),
                    sType = ds.Tables[0].Rows[0]["sType"].ToString(),
                    dtil = ds.Tables[0].Rows[0]["dtil"].ToString(),
                    sPhone = ds.Tables[0].Rows[0]["sPhone"].ToString(),
                    srcDtil = ds.Tables[0].Rows[0]["srchDtil"].ToString(),
                    statDt= DateTime.Parse(ds.Tables[0].Rows[0]["statDt"].ToString()),
                    expDt = DateTime.Parse(ds.Tables[0].Rows[0]["expDt"].ToString())
                };
            return service;
        }
       
        public int SetService(Tracker.Entity.web.Service service)
        {
            Tracker.Entity.web.Service tService = null;
            if (service.sid>0  )
                tService = GetService(service.sid);
            if (tService == null)
            {
                DBConnect db = new DBConnect();
                string insert = string.Format("Insert into tblService (uid,sType,sPhone,srchDtil,dtil,statDt,expDt) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');", service.uid,service.sType, service.sPhone,service.srcDtil,service.dtil,service.statDt.ToString("yyyy-MM-dd"),service.expDt.ToString("yyyy-MM-dd"));
                return db.Insert(insert);
            }
            else
                throw new Exception("Service Already Found.");

        }
        public int UpdService(Tracker.Entity.web.Service service)
        {
           
            if (service != null && service.sid>0)
            {
                DBConnect db = new DBConnect();
                string updateStatement = string.Format("Update tblService set suspDt='{0}' where sid='{1}';",DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),service.sid);
                db.Insert(updateStatement);
                return SetService(service);
            }
            else
                throw new Exception("Service Unavailalbe to modify..");
        }
    }
}
