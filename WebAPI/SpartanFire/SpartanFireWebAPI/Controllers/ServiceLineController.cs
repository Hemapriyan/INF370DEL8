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
    public class ServiceLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ServiceLine
        public IQueryable<ServiceLine> GetServiceLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ServiceLines;
        }

        // GET: api/ServiceLine/5
        [ResponseType(typeof(ServiceLine))]
        public IHttpActionResult GetServiceLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceLine serviceLine = db.ServiceLines.Find(id);
            if (serviceLine == null)
            {
                return NotFound();
            }

            return Ok(serviceLine);
        }

        // PUT: api/ServiceLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServiceLine(int id, ServiceLine serviceLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceLine.ProductID)
            {
                return BadRequest();
            }

            db.Entry(serviceLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceLineExists(id))
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

        // POST: api/ServiceLine
        [ResponseType(typeof(ServiceLine))]
        public IHttpActionResult PostServiceLine(ServiceLine serviceLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceLines.Add(serviceLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ServiceLineExists(serviceLine.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = serviceLine.ProductID }, serviceLine);
        }

        // DELETE: api/ServiceLine/5
        [ResponseType(typeof(ServiceLine))]
        public IHttpActionResult DeleteServiceLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceLine serviceLine = db.ServiceLines.Find(id);
            if (serviceLine == null)
            {
                return NotFound();
            }

            db.ServiceLines.Remove(serviceLine);
            db.SaveChanges();

            return Ok(serviceLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceLineExists(int id)
        {
            return db.ServiceLines.Count(e => e.ProductID == id) > 0;
        }
    }
}