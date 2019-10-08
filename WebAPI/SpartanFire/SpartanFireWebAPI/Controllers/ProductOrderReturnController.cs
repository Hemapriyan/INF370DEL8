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
    public class ProductOrderReturnController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ProductOrderReturn
        public IQueryable<ProductOrderReturn> GetProductOrderReturns()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ProductOrderReturns;
        }

        // GET: api/ProductOrderReturn/5
        [ResponseType(typeof(ProductOrderReturn))]
        public IHttpActionResult GetProductOrderReturn(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderReturn productOrderReturn = db.ProductOrderReturns.Find(id);
            if (productOrderReturn == null)
            {
                return NotFound();
            }

            return Ok(productOrderReturn);
        }

        // PUT: api/ProductOrderReturn/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductOrderReturn(int id, ProductOrderReturn productOrderReturn)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productOrderReturn.ProductOrderReturnID)
            {
                return BadRequest();
            }

            db.Entry(productOrderReturn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrderReturnExists(id))
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

        // POST: api/ProductOrderReturn
        [ResponseType(typeof(ProductOrderReturn))]
        public IHttpActionResult PostProductOrderReturn(ProductOrderReturn productOrderReturn)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductOrderReturns.Add(productOrderReturn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productOrderReturn.ProductOrderReturnID }, productOrderReturn);
        }

        // DELETE: api/ProductOrderReturn/5
        [ResponseType(typeof(ProductOrderReturn))]
        public IHttpActionResult DeleteProductOrderReturn(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderReturn productOrderReturn = db.ProductOrderReturns.Find(id);
            if (productOrderReturn == null)
            {
                return NotFound();
            }

            db.ProductOrderReturns.Remove(productOrderReturn);
            db.SaveChanges();

            return Ok(productOrderReturn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductOrderReturnExists(int id)
        {
            return db.ProductOrderReturns.Count(e => e.ProductOrderReturnID == id) > 0;
        }
    }
}