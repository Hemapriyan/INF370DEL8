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
    public class SupplierOrderReturnLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/SupplierOrderReturnLine
        public IQueryable<SupplierOrderReturnLine> GetSupplierOrderReturnLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SupplierOrderReturnLines;
        }

        // GET: api/SupplierOrderReturnLine/5
        [ResponseType(typeof(SupplierOrderReturnLine))]
        public IHttpActionResult GetSupplierOrderReturnLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrderReturnLine supplierOrderReturnLine = db.SupplierOrderReturnLines.Find(id);
            if (supplierOrderReturnLine == null)
            {
                return NotFound();
            }

            return Ok(supplierOrderReturnLine);
        }

        // PUT: api/SupplierOrderReturnLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplierOrderReturnLine(int id, SupplierOrderReturnLine supplierOrderReturnLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplierOrderReturnLine.ReturnID)
            {
                return BadRequest();
            }

            db.Entry(supplierOrderReturnLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierOrderReturnLineExists(id))
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

        // POST: api/SupplierOrderReturnLine
        [ResponseType(typeof(SupplierOrderReturnLine))]
        public IHttpActionResult PostSupplierOrderReturnLine(SupplierOrderReturnLine supplierOrderReturnLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SupplierOrderReturnLines.Add(supplierOrderReturnLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SupplierOrderReturnLineExists(supplierOrderReturnLine.ReturnID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = supplierOrderReturnLine.ReturnID }, supplierOrderReturnLine);
        }

        // DELETE: api/SupplierOrderReturnLine/5
        [ResponseType(typeof(SupplierOrderReturnLine))]
        public IHttpActionResult DeleteSupplierOrderReturnLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrderReturnLine supplierOrderReturnLine = db.SupplierOrderReturnLines.Find(id);
            if (supplierOrderReturnLine == null)
            {
                return NotFound();
            }

            db.SupplierOrderReturnLines.Remove(supplierOrderReturnLine);
            db.SaveChanges();

            return Ok(supplierOrderReturnLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierOrderReturnLineExists(int id)
        {
            return db.SupplierOrderReturnLines.Count(e => e.ReturnID == id) > 0;
        }
    }
}