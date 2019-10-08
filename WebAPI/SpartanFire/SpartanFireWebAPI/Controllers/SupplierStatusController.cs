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
    public class SupplierStatusController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/SupplierStatus
        public IQueryable<SupplierStatu> GetSupplierStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SupplierStatus;
        }

        // GET: api/SupplierStatus/5
        [ResponseType(typeof(SupplierStatu))]
        public IHttpActionResult GetSupplierStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierStatu supplierStatu = db.SupplierStatus.Find(id);
            if (supplierStatu == null)
            {
                return NotFound();
            }

            return Ok(supplierStatu);
        }

        // PUT: api/SupplierStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplierStatu(int id, SupplierStatu supplierStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplierStatu.SupplierStatusID)
            {
                return BadRequest();
            }

            db.Entry(supplierStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierStatuExists(id))
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

        // POST: api/SupplierStatus
        [ResponseType(typeof(SupplierStatu))]
        public IHttpActionResult PostSupplierStatu(SupplierStatu supplierStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SupplierStatus.Add(supplierStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplierStatu.SupplierStatusID }, supplierStatu);
        }

        // DELETE: api/SupplierStatus/5
        [ResponseType(typeof(SupplierStatu))]
        public IHttpActionResult DeleteSupplierStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierStatu supplierStatu = db.SupplierStatus.Find(id);
            if (supplierStatu == null)
            {
                return NotFound();
            }

            db.SupplierStatus.Remove(supplierStatu);
            db.SaveChanges();

            return Ok(supplierStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierStatuExists(int id)
        {
            return db.SupplierStatus.Count(e => e.SupplierStatusID == id) > 0;
        }
    }
}