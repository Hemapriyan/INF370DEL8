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
    public class AssignDeliveryController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/AssignDelivery
        public IQueryable<AssignDelivery> GetAssignDeliveries()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.AssignDeliveries;
        }

        // GET: api/AssignDelivery/5
        [ResponseType(typeof(AssignDelivery))]
        public IHttpActionResult GetAssignDelivery(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AssignDelivery assignDelivery = db.AssignDeliveries.Find(id);
            if (assignDelivery == null)
            {
                return NotFound();
            }

            return Ok(assignDelivery);
        }

        // PUT: api/AssignDelivery/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssignDelivery(int id, AssignDelivery assignDelivery)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assignDelivery.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(assignDelivery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignDeliveryExists(id))
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

        // POST: api/AssignDelivery
        [ResponseType(typeof(AssignDelivery))]
        public IHttpActionResult PostAssignDelivery(AssignDelivery assignDelivery)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssignDeliveries.Add(assignDelivery);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AssignDeliveryExists(assignDelivery.EmployeeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = assignDelivery.EmployeeID }, assignDelivery);
        }

        // DELETE: api/AssignDelivery/5
        [ResponseType(typeof(AssignDelivery))]
        public IHttpActionResult DeleteAssignDelivery(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AssignDelivery assignDelivery = db.AssignDeliveries.Find(id);
            if (assignDelivery == null)
            {
                return NotFound();
            }

            db.AssignDeliveries.Remove(assignDelivery);
            db.SaveChanges();

            return Ok(assignDelivery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignDeliveryExists(int id)
        {
            return db.AssignDeliveries.Count(e => e.EmployeeID == id) > 0;
        }
    }
}