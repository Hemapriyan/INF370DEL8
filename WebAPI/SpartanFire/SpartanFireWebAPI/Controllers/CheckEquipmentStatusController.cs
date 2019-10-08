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
    public class CheckEquipmentStatusController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/CheckEquipmentStatus
        public IQueryable<CheckEquipmentStatu> GetCheckEquipmentStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CheckEquipmentStatus;
        }

        // GET: api/CheckEquipmentStatus/5
        [ResponseType(typeof(CheckEquipmentStatu))]
        public IHttpActionResult GetCheckEquipmentStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipmentStatu checkEquipmentStatu = db.CheckEquipmentStatus.Find(id);
            if (checkEquipmentStatu == null)
            {
                return NotFound();
            }

            return Ok(checkEquipmentStatu);
        }

        // PUT: api/CheckEquipmentStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCheckEquipmentStatu(int id, CheckEquipmentStatu checkEquipmentStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkEquipmentStatu.CheckEquipmentStatusID)
            {
                return BadRequest();
            }

            db.Entry(checkEquipmentStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckEquipmentStatuExists(id))
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

        // POST: api/CheckEquipmentStatus
        [ResponseType(typeof(CheckEquipmentStatu))]
        public IHttpActionResult PostCheckEquipmentStatu(CheckEquipmentStatu checkEquipmentStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckEquipmentStatus.Add(checkEquipmentStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checkEquipmentStatu.CheckEquipmentStatusID }, checkEquipmentStatu);
        }

        // DELETE: api/CheckEquipmentStatus/5
        [ResponseType(typeof(CheckEquipmentStatu))]
        public IHttpActionResult DeleteCheckEquipmentStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipmentStatu checkEquipmentStatu = db.CheckEquipmentStatus.Find(id);
            if (checkEquipmentStatu == null)
            {
                return NotFound();
            }

            db.CheckEquipmentStatus.Remove(checkEquipmentStatu);
            db.SaveChanges();

            return Ok(checkEquipmentStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckEquipmentStatuExists(int id)
        {
            return db.CheckEquipmentStatus.Count(e => e.CheckEquipmentStatusID == id) > 0;
        }
    }
}