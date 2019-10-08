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
    public class ProductStatusController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ProductStatus
        public IQueryable<ProductStatu> GetProductStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ProductStatus;
        }

        // GET: api/ProductStatus/5
        [ResponseType(typeof(ProductStatu))]
        public IHttpActionResult GetProductStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductStatu productStatu = db.ProductStatus.Find(id);
            if (productStatu == null)
            {
                return NotFound();
            }

            return Ok(productStatu);
        }

        // PUT: api/ProductStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductStatu(int id, ProductStatu productStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productStatu.ProductStatusID)
            {
                return BadRequest();
            }

            db.Entry(productStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatuExists(id))
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

        // POST: api/ProductStatus
        [ResponseType(typeof(ProductStatu))]
        public IHttpActionResult PostProductStatu(ProductStatu productStatu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductStatus.Add(productStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productStatu.ProductStatusID }, productStatu);
        }

        // DELETE: api/ProductStatus/5
        [ResponseType(typeof(ProductStatu))]
        public IHttpActionResult DeleteProductStatu(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductStatu productStatu = db.ProductStatus.Find(id);
            if (productStatu == null)
            {
                return NotFound();
            }

            db.ProductStatus.Remove(productStatu);
            db.SaveChanges();

            return Ok(productStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductStatuExists(int id)
        {
            return db.ProductStatus.Count(e => e.ProductStatusID == id) > 0;
        }
    }
}