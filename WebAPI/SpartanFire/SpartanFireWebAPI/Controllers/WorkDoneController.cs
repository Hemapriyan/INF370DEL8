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
    public class WorkDoneController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/WorkDone
        public IQueryable<WorkDone> GetWorkDones()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.WorkDones;
        }

        // GET: api/WorkDone/5
        [ResponseType(typeof(WorkDone))]
        public IHttpActionResult GetWorkDone(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            WorkDone workDone = db.WorkDones.Find(id);
            if (workDone == null)
            {
                return NotFound();
            }

            return Ok(workDone);
        }

        // PUT: api/WorkDone/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkDone(int id, WorkDone workDone)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workDone.WorkDoneID)
            {
                return BadRequest();
            }

            db.Entry(workDone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDoneExists(id))
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

        // POST: api/WorkDone
        [ResponseType(typeof(WorkDone))]
        public IHttpActionResult PostWorkDone(WorkDone workDone)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkDones.Add(workDone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workDone.WorkDoneID }, workDone);
        }

        // DELETE: api/WorkDone/5
        [ResponseType(typeof(WorkDone))]
        public IHttpActionResult DeleteWorkDone(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            WorkDone workDone = db.WorkDones.Find(id);
            if (workDone == null)
            {
                return NotFound();
            }

            db.WorkDones.Remove(workDone);
            db.SaveChanges();

            return Ok(workDone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkDoneExists(int id)
        {
            return db.WorkDones.Count(e => e.WorkDoneID == id) > 0;
        }
    }
}