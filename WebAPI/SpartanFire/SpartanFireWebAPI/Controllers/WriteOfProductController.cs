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
    public class WriteOfProductController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/WriteOfProduct
        public IQueryable<WriteOfProduct> GetWriteOfProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.WriteOfProducts;
        }

        // GET: api/WriteOfProduct/5
        [ResponseType(typeof(WriteOfProduct))]
        public IHttpActionResult GetWriteOfProduct(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            WriteOfProduct writeOfProduct = db.WriteOfProducts.Find(id);
            if (writeOfProduct == null)
            {
                return NotFound();
            }

            return Ok(writeOfProduct);
        }

        // PUT: api/WriteOfProduct/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWriteOfProduct(int id, WriteOfProduct writeOfProduct)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != writeOfProduct.WriteOfID)
            {
                return BadRequest();
            }

            db.Entry(writeOfProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WriteOfProductExists(id))
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

        // POST: api/WriteOfProduct
        [ResponseType(typeof(WriteOfProduct))]
        public IHttpActionResult PostWriteOfProduct(WriteOfProduct writeOfProduct)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WriteOfProducts.Add(writeOfProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = writeOfProduct.WriteOfID }, writeOfProduct);
        }

        // DELETE: api/WriteOfProduct/5
        [ResponseType(typeof(WriteOfProduct))]
        public IHttpActionResult DeleteWriteOfProduct(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            WriteOfProduct writeOfProduct = db.WriteOfProducts.Find(id);
            if (writeOfProduct == null)
            {
                return NotFound();
            }

            db.WriteOfProducts.Remove(writeOfProduct);
            db.SaveChanges();

            return Ok(writeOfProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WriteOfProductExists(int id)
        {
            return db.WriteOfProducts.Count(e => e.WriteOfID == id) > 0;
        }
    }
}