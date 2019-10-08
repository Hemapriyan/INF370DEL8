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
    public class MobileDeliveryController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "all", "good" };
        }

        // GET api/<controller>/5
        public IEnumerable<Delivery> Get(int id)
        {
           db.Configuration.ProxyCreationEnabled = false;
           return db.AssignDeliveries
           //     .Where(d=>d.EmployeeID==id)
                .Select(ad=>ad.Delivery)
                .Include(ad=>ad.ProductOrder.Location)
           //     .Where(ad=>ad.DateOfDelivery == DateTime.Today)
                .ToList();
        }

    }
}