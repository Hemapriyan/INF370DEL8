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
    public class ServiceItemTypeController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ServiceItemType
        public IQueryable<ServiceItemType> GetServiceItemTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceItemTypes;
        }

        // GET: api/ServiceItemType/5
        [ResponseType(typeof(ServiceItemType))]
        public IHttpActionResult GetServiceItemType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceItemType serviceItemType = db.ServiceItemTypes.Find(id);
            if (serviceItemType == null)
            {
                return NotFound();
            }

            return Ok(serviceItemType);
        }

        // PUT: api/ServiceItemType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceItemType(int id, ServiceItemType serviceItemType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceItemType.ServiceItemTypeID)
            {
                return BadRequest();
            }

            db.Entry(serviceItemType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceItemTypeExists(id))
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

        // POST: api/ServiceItemType
        [ResponseType(typeof(ServiceItemType))]
        public IHttpActionResult PostServiceItemType(ServiceItemType serviceItemType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceItemTypes.Add(serviceItemType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = serviceItemType.ServiceItemTypeID }, serviceItemType);
        }

        // DELETE: api/ServiceItemType/5
        [ResponseType(typeof(ServiceItemType))]
        public IHttpActionResult DeleteServiceItemType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceItemType serviceItemType = db.ServiceItemTypes.Find(id);
            if (serviceItemType == null)
            {
                return NotFound();
            }

            db.ServiceItemTypes.Remove(serviceItemType);
            db.SaveChanges();

            return Ok(serviceItemType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceItemTypeExists(int id)
        {
            return db.ServiceItemTypes.Count(e => e.ServiceItemTypeID == id) > 0;
        }
    }
}