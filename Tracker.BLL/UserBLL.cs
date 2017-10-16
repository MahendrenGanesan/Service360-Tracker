using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.DAL;
using System.Data;
namespace Tracker.BLL
{
    public class UserBLL
    {
        public static UserBLL Instance { get { return new UserBLL(); } }
        public Tracker.Entity.web.User GetUser(int id)
        {
            DBConnect db = new DBConnect();
            DataSet ds = db.Select("select * from tblUser where uid=" + id.ToString());
            Tracker.Entity.web.User user = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                user = new Entity.web.User
                {
                uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString()),
                details = ds.Tables[0].Rows[0]["details"].ToString(),
                Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                Phone = ds.Tables[0].Rows[0]["Phone"].ToString(),
                suspDt = ds.Tables[0].Rows[0]["suspDt"].ToString() != "" ?
                        DateTime.Parse(ds.Tables[0].Rows[0]["suspDt"].ToString()) : DateTime.MinValue
            };
            return user;
        }
        public Tracker.Entity.web.User GetUserByMail(string email)
        {
            DBConnect db = new DBConnect();
            DataSet ds = db.Select(string.Format("select * from tblUser where Email='{0}'", email));
            Tracker.Entity.web.User user = null;
            if(ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            user=new Entity.web.User
            {
                uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString()),
                details = ds.Tables[0].Rows[0]["details"].ToString(),
                Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                Phone = ds.Tables[0].Rows[0]["Phone"].ToString(),
                suspDt = ds.Tables[0].Rows[0]["suspDt"].ToString() != "" ?
                        DateTime.Parse(ds.Tables[0].Rows[0]["suspDt"].ToString()) : DateTime.MinValue
            };
            return user;
        }
        public Tracker.Entity.web.User GetUserByPhone(string phone)
        {
            DBConnect db = new DBConnect();
            DataSet ds = db.Select(string.Format("select * from tblUser where Phone='{0}'", phone));
            Tracker.Entity.web.User user = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                user = new Entity.web.User
                {
                uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString()),
                details = ds.Tables[0].Rows[0]["details"].ToString(),
                Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                Phone = ds.Tables[0].Rows[0]["Phone"].ToString(),
                suspDt = ds.Tables[0].Rows[0]["suspDt"].ToString() != "" ?
                        DateTime.Parse(ds.Tables[0].Rows[0]["suspDt"].ToString()) : DateTime.MinValue
            };
            return user;
        }

        public Tracker.Entity.web.User GetUserByPhoneOrByMail(string phone,string email)
        {
            DBConnect db = new DBConnect();
            DataSet ds = db.Select(string.Format("select * from tblUser where Phone='{0}' or Email='{0}'", phone, email));
            Tracker.Entity.web.User user = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                user = new Entity.web.User
                {
                    uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString()),
                    details = ds.Tables[0].Rows[0]["details"].ToString(),
                    Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                    Phone = ds.Tables[0].Rows[0]["Phone"].ToString(),
                    suspDt = ds.Tables[0].Rows[0]["suspDt"].ToString() != "" ?
                        DateTime.Parse(ds.Tables[0].Rows[0]["suspDt"].ToString()) : DateTime.MinValue
                };
            return user;
        }
        public int SetUser(Tracker.Entity.web.User user)
        {
            Tracker.Entity.web.User usr= GetUserByPhoneOrByMail(user.Phone,user.Email);
            if (usr == null)
            {
                DBConnect db = new DBConnect();
                string insert = string.Format("Insert into tblUser (details,Email,Phone) values('{0}','{1}','{2}');", user.details, user.Email, user.Phone);
                return db.Insert(insert);
            }
            else
                throw new Exception("User e-mail/phone number Id found..");

        }
        public int UpdUser(Tracker.Entity.web.User user)
        {
            Tracker.Entity.web.User usr = GetUserByPhoneOrByMail(user.Phone, user.Email);
            if (usr == null)
            {
                DBConnect db = new DBConnect();
                string insert = string.Format("Update tblUser set details='{0}' where email='{1}';", user.details, user.Email);
                return db.Insert(insert);
            }
            else
                throw new Exception("User e-mail/phone number Id found..");
        }
    }
}
