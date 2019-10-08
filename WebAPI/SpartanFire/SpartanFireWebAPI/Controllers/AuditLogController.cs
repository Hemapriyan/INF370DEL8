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
    public class AuditLogController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/AuditLog
        public IQueryable<AuditLog> GetAuditLogs()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.AuditLogs;
        }

        // GET: api/AuditLog/5
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult GetAuditLog(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AuditLog auditLog = db.AuditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return Ok(auditLog);
        }

        // PUT: api/AuditLog/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuditLog(int id, AuditLog auditLog)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditLog.AuditLogID)
            {
                return BadRequest();
            }

            db.Entry(auditLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(id))
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

        // POST: api/AuditLog
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult PostAuditLog(AuditLog auditLog)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuditLogs.Add(auditLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = auditLog.AuditLogID }, auditLog);
        }

        // DELETE: api/AuditLog/5
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult DeleteAuditLog(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AuditLog auditLog = db.AuditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            db.AuditLogs.Remove(auditLog);
            db.SaveChanges();

            return Ok(auditLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditLogExists(int id)
        {
            return db.AuditLogs.Count(e => e.AuditLogID == id) > 0;
        }
    }
}