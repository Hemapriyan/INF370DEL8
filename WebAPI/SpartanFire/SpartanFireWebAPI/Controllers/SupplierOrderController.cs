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
    public class SupplierOrderController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/SupplierOrder
        public IQueryable<SupplierOrder> GetSupplierOrders()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SupplierOrders;
        }

        // GET: api/SupplierOrder/5
        [ResponseType(typeof(SupplierOrder))]
        public IHttpActionResult GetSupplierOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrder supplierOrder = db.SupplierOrders.Find(id);
            if (supplierOrder == null)
            {
                return NotFound();
            }

            return Ok(supplierOrder);
        }

        // PUT: api/SupplierOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplierOrder(int id, SupplierOrder supplierOrder)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplierOrder.SupplierOrderID)
            {
                return BadRequest();
            }

            db.Entry(supplierOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierOrderExists(id))
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

        // POST: api/SupplierOrder
        [ResponseType(typeof(SupplierOrder))]
        public IHttpActionResult PostSupplierOrder(SupplierOrder supplierOrder)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SupplierOrders.Add(supplierOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplierOrder.SupplierOrderID }, supplierOrder);
        }

        // DELETE: api/SupplierOrder/5
        [ResponseType(typeof(SupplierOrder))]
        public IHttpActionResult DeleteSupplierOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrder supplierOrder = db.SupplierOrders.Find(id);
            if (supplierOrder == null)
            {
                return NotFound();
            }

            db.SupplierOrders.Remove(supplierOrder);
            db.SaveChanges();

            return Ok(supplierOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierOrderExists(int id)
        {
            return db.SupplierOrders.Count(e => e.SupplierOrderID == id) > 0;
        }
    }
}