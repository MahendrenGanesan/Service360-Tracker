using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracker.BLL;
using Tracker.Entity.web;

namespace TrackerWeb.ApiControllers
{
    public class userController : ApiController
    {
        [ActionName("GetById")]
        public User Get(int id)
        {
            var retObj=UserBLL.Instance.GetUser(id);
            return retObj;
        }

        [ActionName("GetByMail")]
        public User GetByMail(string email)
        {
            var retObj = UserBLL.Instance.GetUserByMail(email);
            return retObj;
        }

        // POST api/<controller>
        public void Post([FromBody]User usr)
        {
           UserBLL.Instance.SetUser(usr);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}