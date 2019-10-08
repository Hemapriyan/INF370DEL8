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
    public class SupplierOrderLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/SupplierOrderLine
        public IQueryable<SupplierOrderLine> GetSupplierOrderLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SupplierOrderLines;
        }

        // GET: api/SupplierOrderLine/5
        [ResponseType(typeof(SupplierOrderLine))]
        public IHttpActionResult GetSupplierOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrderLine supplierOrderLine = db.SupplierOrderLines.Find(id);
            if (supplierOrderLine == null)
            {
                return NotFound();
            }

            return Ok(supplierOrderLine);
        }

        // PUT: api/SupplierOrderLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplierOrderLine(int id, SupplierOrderLine supplierOrderLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplierOrderLine.ProductID)
            {
                return BadRequest();
            }

            db.Entry(supplierOrderLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierOrderLineExists(id))
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

        // POST: api/SupplierOrderLine
        [ResponseType(typeof(SupplierOrderLine))]
        public IHttpActionResult PostSupplierOrderLine(SupplierOrderLine supplierOrderLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SupplierOrderLines.Add(supplierOrderLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SupplierOrderLineExists(supplierOrderLine.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = supplierOrderLine.ProductID }, supplierOrderLine);
        }

        // DELETE: api/SupplierOrderLine/5
        [ResponseType(typeof(SupplierOrderLine))]
        public IHttpActionResult DeleteSupplierOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrderLine supplierOrderLine = db.SupplierOrderLines.Find(id);
            if (supplierOrderLine == null)
            {
                return NotFound();
            }

            db.SupplierOrderLines.Remove(supplierOrderLine);
            db.SaveChanges();

            return Ok(supplierOrderLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierOrderLineExists(int id)
        {
            return db.SupplierOrderLines.Count(e => e.ProductID == id) > 0;
        }
    }
}