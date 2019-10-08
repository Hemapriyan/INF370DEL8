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
    public class SupplierOrderReturnController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/SupplierOrderReturn
        public IQueryable<SupplierOrderReturn> GetSupplierOrderReturns()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SupplierOrderReturns;
        }

        // GET: api/SupplierOrderReturn/5
        [ResponseType(typeof(SupplierOrderReturn))]
        public IHttpActionResult GetSupplierOrderReturn(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrderReturn supplierOrderReturn = db.SupplierOrderReturns.Find(id);
            if (supplierOrderReturn == null)
            {
                return NotFound();
            }

            return Ok(supplierOrderReturn);
        }

        // PUT: api/SupplierOrderReturn/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplierOrderReturn(int id, SupplierOrderReturn supplierOrderReturn)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplierOrderReturn.ReturnID)
            {
                return BadRequest();
            }

            db.Entry(supplierOrderReturn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierOrderReturnExists(id))
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

        // POST: api/SupplierOrderReturn
        [ResponseType(typeof(SupplierOrderReturn))]
        public IHttpActionResult PostSupplierOrderReturn(SupplierOrderReturn supplierOrderReturn)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SupplierOrderReturns.Add(supplierOrderReturn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplierOrderReturn.ReturnID }, supplierOrderReturn);
        }

        // DELETE: api/SupplierOrderReturn/5
        [ResponseType(typeof(SupplierOrderReturn))]
        public IHttpActionResult DeleteSupplierOrderReturn(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrderReturn supplierOrderReturn = db.SupplierOrderReturns.Find(id);
            if (supplierOrderReturn == null)
            {
                return NotFound();
            }

            db.SupplierOrderReturns.Remove(supplierOrderReturn);
            db.SaveChanges();

            return Ok(supplierOrderReturn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierOrderReturnExists(int id)
        {
            return db.SupplierOrderReturns.Count(e => e.ReturnID == id) > 0;
        }
    }
}