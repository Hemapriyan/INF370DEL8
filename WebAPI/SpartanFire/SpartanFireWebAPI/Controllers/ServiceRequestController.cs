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
    public class ServiceRequestController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ServiceRequest
        public IQueryable<ServiceRequest> GetServiceRequests()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceRequests;
        }

        // GET: api/ServiceRequest/5
        [ResponseType(typeof(ServiceRequest))]
        public IHttpActionResult GetServiceRequest(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            return Ok(serviceRequest);
        }

        // PUT: api/ServiceRequest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceRequest(int id, ServiceRequest serviceRequest)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceRequest.ServiceRequestID)
            {
                return BadRequest();
            }
            var temp = db.ServiceRequests.Find(id);
            serviceRequest.ServiceRequestDate = temp.ServiceRequestDate;
            serviceRequest.ServiceRequestStatusID = temp.ServiceRequestStatusID;

            db.Entry(serviceRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
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

        // POST: api/ServiceRequest
        [ResponseType(typeof(ServiceRequest))]
        public IHttpActionResult PostServiceRequest(ServiceRequest serviceRequest)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceRequests.Add(serviceRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = serviceRequest.ServiceRequestID }, serviceRequest);
        }

        // DELETE: api/ServiceRequest/5
        [ResponseType(typeof(ServiceRequest))]
        public IHttpActionResult DeleteServiceRequest(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            db.ServiceRequests.Remove(serviceRequest);
            db.SaveChanges();

            return Ok(serviceRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceRequestExists(int id)
        {
            return db.ServiceRequests.Count(e => e.ServiceRequestID == id) > 0;
        }
    }
}