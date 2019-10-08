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
    public class ServiceRequestStatusController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ServiceRequestStatus
        public IQueryable<ServiceRequestStatu> GetServiceRequestStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceRequestStatus;
        }

        // GET: api/ServiceRequestStatus/5
        [ResponseType(typeof(ServiceRequestStatu))]
        public IHttpActionResult GetServiceRequestStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequestStatu serviceRequestStatu = db.ServiceRequestStatus.Find(id);
            if (serviceRequestStatu == null)
            {
                return NotFound();
            }

            return Ok(serviceRequestStatu);
        }

        // PUT: api/ServiceRequestStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceRequestStatu(int id, ServiceRequestStatu serviceRequestStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceRequestStatu.ServiceRequestStatusID)
            {
                return BadRequest();
            }

            db.Entry(serviceRequestStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestStatuExists(id))
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

        // POST: api/ServiceRequestStatus
        [ResponseType(typeof(ServiceRequestStatu))]
        public IHttpActionResult PostServiceRequestStatu(ServiceRequestStatu serviceRequestStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceRequestStatus.Add(serviceRequestStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = serviceRequestStatu.ServiceRequestStatusID }, serviceRequestStatu);
        }

        // DELETE: api/ServiceRequestStatus/5
        [ResponseType(typeof(ServiceRequestStatu))]
        public IHttpActionResult DeleteServiceRequestStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequestStatu serviceRequestStatu = db.ServiceRequestStatus.Find(id);
            if (serviceRequestStatu == null)
            {
                return NotFound();
            }

            db.ServiceRequestStatus.Remove(serviceRequestStatu);
            db.SaveChanges();

            return Ok(serviceRequestStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceRequestStatuExists(int id)
        {
            return db.ServiceRequestStatus.Count(e => e.ServiceRequestStatusID == id) > 0;
        }
    }
}