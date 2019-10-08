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
    public class CheckEquipmentLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/CheckEquipmentLine
        public IQueryable<CheckEquipmentLine> GetCheckEquipmentLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CheckEquipmentLines;
        }

        // GET: api/CheckEquipmentLine/5
        [ResponseType(typeof(CheckEquipmentLine))]
        public IHttpActionResult GetCheckEquipmentLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipmentLine checkEquipmentLine = db.CheckEquipmentLines.Find(id);
            if (checkEquipmentLine == null)
            {
                return NotFound();
            }

            return Ok(checkEquipmentLine);
        }

        // PUT: api/CheckEquipmentLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCheckEquipmentLine(int id, CheckEquipmentLine checkEquipmentLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkEquipmentLine.EquipmentID)
            {
                return BadRequest();
            }

            db.Entry(checkEquipmentLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckEquipmentLineExists(id))
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

        // POST: api/CheckEquipmentLine
        [ResponseType(typeof(CheckEquipmentLine))]
        public IHttpActionResult PostCheckEquipmentLine(CheckEquipmentLine checkEquipmentLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckEquipmentLines.Add(checkEquipmentLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CheckEquipmentLineExists(checkEquipmentLine.EquipmentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = checkEquipmentLine.EquipmentID }, checkEquipmentLine);
        }

        // DELETE: api/CheckEquipmentLine/5
        [ResponseType(typeof(CheckEquipmentLine))]
        public IHttpActionResult DeleteCheckEquipmentLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipmentLine checkEquipmentLine = db.CheckEquipmentLines.Find(id);
            if (checkEquipmentLine == null)
            {
                return NotFound();
            }

            db.CheckEquipmentLines.Remove(checkEquipmentLine);
            db.SaveChanges();

            return Ok(checkEquipmentLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckEquipmentLineExists(int id)
        {
            return db.CheckEquipmentLines.Count(e => e.EquipmentID == id) > 0;
        }
    }
}