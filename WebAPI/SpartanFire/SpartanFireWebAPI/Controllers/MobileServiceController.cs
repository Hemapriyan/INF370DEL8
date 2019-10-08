using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SpartanFireWebAPI.Models;

namespace SpartanFireWebAPI.Controllers
{

    public class MobileServiceController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IEnumerable<ServiceRequest> Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.EmployeeRequests
          //      .Where(d => d.EmployeeID == id)
                 .Select(ad => ad.ServiceRequest)
                 .Include(ad=>ad.Location)
                 .Include(ad=>ad.Location.Client)                                                                                                   
          //       .Where(ad => ad.ServiceBookedDate == DateTime.Today)
                 .ToList();
        }

    }
}