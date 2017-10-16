using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracker.BLL;
namespace TrackerWeb.ApiControllers
{
    public class serviceController : ApiController
    {
        
        // GET: api/service/5
        public Tracker.Entity.web.Service Get(int id)
        {
           return ServiceBLL.Instance.GetService(id);
        }

        // POST: api/service
        public int Post([FromBody]Tracker.Entity.web.Service service)
        {
            return ServiceBLL.Instance.SetService(service);
        }

        // PUT: api/service/5
        public int Put(int id, [FromBody]Tracker.Entity.web.Service service)
        {
            service.sid = id;
            return ServiceBLL.Instance.UpdService(service);
        }
    }
}
