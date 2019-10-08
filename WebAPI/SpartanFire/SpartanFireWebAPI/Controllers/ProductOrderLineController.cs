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
    public class ProductOrderLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ProductOrderLine
        public IQueryable<ProductOrderLine> GetProductOrderLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ProductOrderLines;
        }

        // GET: api/ProductOrderLine/5
        [ResponseType(typeof(ProductOrderLine))]
        public IHttpActionResult GetProductOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderLine productOrderLine = db.ProductOrderLines.Find(id);
            if (productOrderLine == null)
            {
                return NotFound();
            }

            return Ok(productOrderLine);
        }

        // PUT: api/ProductOrderLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductOrderLine(int id, ProductOrderLine productOrderLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productOrderLine.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productOrderLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrderLineExists(id))
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

        // POST: api/ProductOrderLine
        [ResponseType(typeof(ProductOrderLine))]
        public IHttpActionResult PostProductOrderLine(ProductOrderLine productOrderLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductOrderLines.Add(productOrderLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductOrderLineExists(productOrderLine.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productOrderLine.ProductID }, productOrderLine);
        }

        // DELETE: api/ProductOrderLine/5
        [ResponseType(typeof(ProductOrderLine))]
        public IHttpActionResult DeleteProductOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderLine productOrderLine = db.ProductOrderLines.Find(id);
            if (productOrderLine == null)
            {
                return NotFound();
            }

            db.ProductOrderLines.Remove(productOrderLine);
            db.SaveChanges();

            return Ok(productOrderLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductOrderLineExists(int id)
        {
            return db.ProductOrderLines.Count(e => e.ProductID == id) > 0;
        }
    }
}