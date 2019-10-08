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
    public class DeliveryStatusController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/DeliveryStatus
        public IQueryable<DeliveryStatu> GetDeliveryStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.DeliveryStatus;
        }

        // GET: api/DeliveryStatus/5
        [ResponseType(typeof(DeliveryStatu))]
        public IHttpActionResult GetDeliveryStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DeliveryStatu deliveryStatu = db.DeliveryStatus.Find(id);
            if (deliveryStatu == null)
            {
                return NotFound();
            }

            return Ok(deliveryStatu);
        }

        // PUT: api/DeliveryStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeliveryStatu(int id, DeliveryStatu deliveryStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deliveryStatu.DeliveryStatusID)
            {
                return BadRequest();
            }

            db.Entry(deliveryStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryStatuExists(id))
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

        // POST: api/DeliveryStatus
        [ResponseType(typeof(DeliveryStatu))]
        public IHttpActionResult PostDeliveryStatu(DeliveryStatu deliveryStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeliveryStatus.Add(deliveryStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deliveryStatu.DeliveryStatusID }, deliveryStatu);
        }

        // DELETE: api/DeliveryStatus/5
        [ResponseType(typeof(DeliveryStatu))]
        public IHttpActionResult DeleteDeliveryStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DeliveryStatu deliveryStatu = db.DeliveryStatus.Find(id);
            if (deliveryStatu == null)
            {
                return NotFound();
            }

            db.DeliveryStatus.Remove(deliveryStatu);
            db.SaveChanges();

            return Ok(deliveryStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryStatuExists(int id)
        {
            return db.DeliveryStatus.Count(e => e.DeliveryStatusID == id) > 0;
        }
    }
}