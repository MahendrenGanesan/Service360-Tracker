using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tracker.BLL;
namespace TrackerWeb.ApiControllers
{
    public class gioPositionController : ApiController
    {
       
        // GET: api/GioPosition/5
        public IList<Tracker.Entity.web.GioPosition> Get(int id)
        {
           return GioPositionBLL.Instance.GetGioPosition(5, id);
        }
        // POST: api/GioPosition
        public int Post([FromBody] Tracker.Entity.web.GioPosition GioPosition)
        {
            return GioPositionBLL.Instance.SetGioPosition(GioPosition);
        }
        
    }
}
