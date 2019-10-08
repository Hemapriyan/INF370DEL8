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
    public class VATController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/VAT
        public IQueryable<VAT> GetVATs()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.VATs;
        }

        // GET: api/VAT/5
        [ResponseType(typeof(VAT))]
        public IHttpActionResult GetVAT(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            VAT vAT = db.VATs.Find(id);
            if (vAT == null)
            {
                return NotFound();
            }

            return Ok(vAT);
        }

        // PUT: api/VAT/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVAT(int id, VAT vAT)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vAT.VATID)
            {
                return BadRequest();
            }

            db.Entry(vAT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VATExists(id))
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

        // POST: api/VAT
        [ResponseType(typeof(VAT))]
        public IHttpActionResult PostVAT(VAT vAT)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VATs.Add(vAT);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vAT.VATID }, vAT);
        }

        // DELETE: api/VAT/5
        [ResponseType(typeof(VAT))]
        public IHttpActionResult DeleteVAT(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            VAT vAT = db.VATs.Find(id);
            if (vAT == null)
            {
                return NotFound();
            }

            db.VATs.Remove(vAT);
            db.SaveChanges();

            return Ok(vAT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VATExists(int id)
        {
            return db.VATs.Count(e => e.VATID == id) > 0;
        }
    }
}