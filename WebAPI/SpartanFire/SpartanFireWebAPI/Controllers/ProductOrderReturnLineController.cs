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
    public class ProductOrderReturnLineController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ProductOrderReturnLine
        public IQueryable<ProductOrderReturnLine> GetProductOrderReturnLines()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ProductOrderReturnLines;
        }

        // GET: api/ProductOrderReturnLine/5
        [ResponseType(typeof(ProductOrderReturnLine))]
        public IHttpActionResult GetProductOrderReturnLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderReturnLine productOrderReturnLine = db.ProductOrderReturnLines.Find(id);
            if (productOrderReturnLine == null)
            {
                return NotFound();
            }

            return Ok(productOrderReturnLine);
        }

        // PUT: api/ProductOrderReturnLine/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductOrderReturnLine(int id, ProductOrderReturnLine productOrderReturnLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productOrderReturnLine.ProductOrderReturnID)
            {
                return BadRequest();
            }

            db.Entry(productOrderReturnLine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrderReturnLineExists(id))
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

        // POST: api/ProductOrderReturnLine
        [ResponseType(typeof(ProductOrderReturnLine))]
        public IHttpActionResult PostProductOrderReturnLine(ProductOrderReturnLine productOrderReturnLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductOrderReturnLines.Add(productOrderReturnLine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductOrderReturnLineExists(productOrderReturnLine.ProductOrderReturnID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productOrderReturnLine.ProductOrderReturnID }, productOrderReturnLine);
        }

        // DELETE: api/ProductOrderReturnLine/5
        [ResponseType(typeof(ProductOrderReturnLine))]
        public IHttpActionResult DeleteProductOrderReturnLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderReturnLine productOrderReturnLine = db.ProductOrderReturnLines.Find(id);
            if (productOrderReturnLine == null)
            {
                return NotFound();
            }

            db.ProductOrderReturnLines.Remove(productOrderReturnLine);
            db.SaveChanges();

            return Ok(productOrderReturnLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductOrderReturnLineExists(int id)
        {
            return db.ProductOrderReturnLines.Count(e => e.ProductOrderReturnID == id) > 0;
        }
    }
}