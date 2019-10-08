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
    public class ServiceItemSizeController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ServiceItemSize
        public IQueryable<ServiceItemSize> GetServiceItemSizes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceItemSizes;
        }

        // GET: api/ServiceItemSize/5
        [ResponseType(typeof(ServiceItemSize))]
        public IHttpActionResult GetServiceItemSize(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceItemSize serviceItemSize = db.ServiceItemSizes.Find(id);
            if (serviceItemSize == null)
            {
                return NotFound();
            }

            return Ok(serviceItemSize);
        }

        // PUT: api/ServiceItemSize/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceItemSize(int id, ServiceItemSize serviceItemSize)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceItemSize.ServiceItemSizeID)
            {
                return BadRequest();
            }

            db.Entry(serviceItemSize).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceItemSizeExists(id))
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

        // POST: api/ServiceItemSize
        [ResponseType(typeof(ServiceItemSize))]
        public IHttpActionResult PostServiceItemSize(ServiceItemSize serviceItemSize)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceItemSizes.Add(serviceItemSize);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = serviceItemSize.ServiceItemSizeID }, serviceItemSize);
        }

        // DELETE: api/ServiceItemSize/5
        [ResponseType(typeof(ServiceItemSize))]
        public IHttpActionResult DeleteServiceItemSize(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceItemSize serviceItemSize = db.ServiceItemSizes.Find(id);
            if (serviceItemSize == null)
            {
                return NotFound();
            }

            db.ServiceItemSizes.Remove(serviceItemSize);
            db.SaveChanges();

            return Ok(serviceItemSize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceItemSizeExists(int id)
        {
            return db.ServiceItemSizes.Count(e => e.ServiceItemSizeID == id) > 0;
        }
    }
}