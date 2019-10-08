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
    public class DeliveryLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/DeliveryLine
        public IQueryable<DeliveryLine> GetDeliveryLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.DeliveryLines;
        }

        // GET: api/DeliveryLine/5
        [ResponseType(typeof(DeliveryLine))]
        public IHttpActionResult GetDeliveryLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DeliveryLine deliveryLine = db.DeliveryLines.Find(id);
            if (deliveryLine == null)
            {
                return NotFound();
            }

            return Ok(deliveryLine);
        }

        // PUT: api/DeliveryLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeliveryLine(int id, DeliveryLine deliveryLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deliveryLine.DeliveryID)
            {
                return BadRequest();
            }

            db.Entry(deliveryLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryLineExists(id))
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

        // POST: api/DeliveryLine
        [ResponseType(typeof(DeliveryLine))]
        public IHttpActionResult PostDeliveryLine(DeliveryLine deliveryLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeliveryLines.Add(deliveryLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeliveryLineExists(deliveryLine.DeliveryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deliveryLine.DeliveryID }, deliveryLine);
        }

        // DELETE: api/DeliveryLine/5
        [ResponseType(typeof(DeliveryLine))]
        public IHttpActionResult DeleteDeliveryLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DeliveryLine deliveryLine = db.DeliveryLines.Find(id);
            if (deliveryLine == null)
            {
                return NotFound();
            }

            db.DeliveryLines.Remove(deliveryLine);
            db.SaveChanges();

            return Ok(deliveryLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryLineExists(int id)
        {
            return db.DeliveryLines.Count(e => e.DeliveryID == id) > 0;
        }
    }
}