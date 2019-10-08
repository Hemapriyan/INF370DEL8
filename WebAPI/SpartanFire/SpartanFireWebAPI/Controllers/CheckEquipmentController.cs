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
    public class CheckEquipmentController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/CheckEquipment
        public IQueryable<CheckEquipment> GetCheckEquipments()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CheckEquipments;
        }

        // GET: api/CheckEquipment/5
        [ResponseType(typeof(CheckEquipment))]
        public IHttpActionResult GetCheckEquipment(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipment checkEquipment = db.CheckEquipments.Find(id);
            if (checkEquipment == null)
            {
                return NotFound();
            }

            return Ok(checkEquipment);
        }

        // PUT: api/CheckEquipment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCheckEquipment(int id, CheckEquipment checkEquipment)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkEquipment.CheckEquipmentID)
            {
                return BadRequest();
            }

            db.Entry(checkEquipment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckEquipmentExists(id))
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

        // POST: api/CheckEquipment
        [ResponseType(typeof(CheckEquipment))]
        public IHttpActionResult PostCheckEquipment(CheckEquipment checkEquipment)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckEquipments.Add(checkEquipment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checkEquipment.CheckEquipmentID }, checkEquipment);
        }

        // DELETE: api/CheckEquipment/5
        [ResponseType(typeof(CheckEquipment))]
        public IHttpActionResult DeleteCheckEquipment(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipment checkEquipment = db.CheckEquipments.Find(id);
            if (checkEquipment == null)
            {
                return NotFound();
            }

            db.CheckEquipments.Remove(checkEquipment);
            db.SaveChanges();

            return Ok(checkEquipment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckEquipmentExists(int id)
        {
            return db.CheckEquipments.Count(e => e.CheckEquipmentID == id) > 0;
        }
    }
}