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
    public class ProductOrderStatusController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ProductOrderStatus
        public IQueryable<ProductOrderStatu> GetProductOrderStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ProductOrderStatus;
        }

        // GET: api/ProductOrderStatus/5
        [ResponseType(typeof(ProductOrderStatu))]
        public IHttpActionResult GetProductOrderStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderStatu productOrderStatu = db.ProductOrderStatus.Find(id);
            if (productOrderStatu == null)
            {
                return NotFound();
            }

            return Ok(productOrderStatu);
        }

        // PUT: api/ProductOrderStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductOrderStatu(int id, ProductOrderStatu productOrderStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productOrderStatu.ProductOrderStatusID)
            {
                return BadRequest();
            }

            db.Entry(productOrderStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrderStatuExists(id))
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

        // POST: api/ProductOrderStatus
        [ResponseType(typeof(ProductOrderStatu))]
        public IHttpActionResult PostProductOrderStatu(ProductOrderStatu productOrderStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductOrderStatus.Add(productOrderStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productOrderStatu.ProductOrderStatusID }, productOrderStatu);
        }

        // DELETE: api/ProductOrderStatus/5
        [ResponseType(typeof(ProductOrderStatu))]
        public IHttpActionResult DeleteProductOrderStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrderStatu productOrderStatu = db.ProductOrderStatus.Find(id);
            if (productOrderStatu == null)
            {
                return NotFound();
            }

            db.ProductOrderStatus.Remove(productOrderStatu);
            db.SaveChanges();

            return Ok(productOrderStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductOrderStatuExists(int id)
        {
            return db.ProductOrderStatus.Count(e => e.ProductOrderStatusID == id) > 0;
        }
    }
}