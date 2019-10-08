using SpartanFireWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpartanFireWebAPI.Controllers
{
    public class MobileDeliveryLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Delivery d = db.Deliveries
                 .Where(ad => ad.DeliveryID == id)
                 .Include(ad => ad.ProductOrder.Location.Client)
                 .FirstOrDefault();
            if (d == null)
            {
                return NotFound();
            }

            return Ok(d);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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