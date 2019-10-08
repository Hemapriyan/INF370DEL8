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
    public class AuditLogTypeController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/AuditLogType
        public IQueryable<AuditLogType> GetAuditLogTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.AuditLogTypes;
        }

        // GET: api/AuditLogType/5
        [ResponseType(typeof(AuditLogType))]
        public IHttpActionResult GetAuditLogType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AuditLogType auditLogType = db.AuditLogTypes.Find(id);
            if (auditLogType == null)
            {
                return NotFound();
            }

            return Ok(auditLogType);
        }

        // PUT: api/AuditLogType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuditLogType(int id, AuditLogType auditLogType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditLogType.AuditLogTypeID)
            {
                return BadRequest();
            }

            db.Entry(auditLogType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogTypeExists(id))
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

        // POST: api/AuditLogType
        [ResponseType(typeof(AuditLogType))]
        public IHttpActionResult PostAuditLogType(AuditLogType auditLogType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuditLogTypes.Add(auditLogType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = auditLogType.AuditLogTypeID }, auditLogType);
        }

        // DELETE: api/AuditLogType/5
        [ResponseType(typeof(AuditLogType))]
        public IHttpActionResult DeleteAuditLogType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AuditLogType auditLogType = db.AuditLogTypes.Find(id);
            if (auditLogType == null)
            {
                return NotFound();
            }

            db.AuditLogTypes.Remove(auditLogType);
            db.SaveChanges();

            return Ok(auditLogType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditLogTypeExists(int id)
        {
            return db.AuditLogTypes.Count(e => e.AuditLogTypeID == id) > 0;
        }
    }
}