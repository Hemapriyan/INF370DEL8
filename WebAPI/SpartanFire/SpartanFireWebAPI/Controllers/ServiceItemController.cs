using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SpartanFireWebAPI.Models;

namespace SpartanFireWebAPI.Controllers
{
    public class ServiceItemController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ServiceItem
        public IQueryable<ServiceItem> GetServiceItems()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceItems;
        }

        // GET: api/ServiceItem/5
        [ResponseType(typeof(ServiceItem))]
        public IHttpActionResult GetServiceItem(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceItem serviceItem = db.ServiceItems.Find(id);
            if (serviceItem == null)
            {
                return NotFound();
            }

            return Ok(serviceItem);
        }

        // PUT: api/ServiceItem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceItem(int id, ServiceItem serviceItem)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceItem.ServiceItemID)
            {
                return BadRequest();
            }

            db.Entry(serviceItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ServiceItem
        [ResponseType(typeof(ServiceItem))]
        public IHttpActionResult PostServiceItem(ServiceItem serviceItem)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceItems.Add(serviceItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = serviceItem.ServiceItemID }, serviceItem);
        }

        // DELETE: api/ServiceItem/5
        [ResponseType(typeof(ServiceItem))]
        public IHttpActionResult DeleteServiceItem(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceItem serviceItem = db.ServiceItems.Find(id);
            if (serviceItem == null)
            {
                return NotFound();
            }

            db.ServiceItems.Remove(serviceItem);
            db.SaveChanges();

            return Ok(serviceItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceItemExists(int id)
        {
            return db.ServiceItems.Count(e => e.ServiceItemID == id) > 0;
        }
    }
}