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
    public class ProductOrderController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/WriteOfProduct
        public IQueryable<ProductOrder> GetProductOrders()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ProductOrders;
        }

        // GET: api/WriteOfProduct/5
        [ResponseType(typeof(ProductOrder))]
        public IHttpActionResult GetProductOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return NotFound();
            }

            return Ok(productOrder);
        }

        // PUT: api/ProductOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductOrder(int id, ProductOrder productOrder)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productOrder.ProductOrderID)
            {
                return BadRequest();
            }

            db.Entry(productOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrderExists(id))
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

        // POST: api/ProductOrder
        [ResponseType(typeof(ProductOrder))]
        public IHttpActionResult PostProductOrder(ProductOrder productOrder)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductOrders.Add(productOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productOrder.ProductOrderID }, productOrder);
        }

        // DELETE: api/ProductOrder/5
        [ResponseType(typeof(ProductOrder))]
        public IHttpActionResult DeleteProductOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return NotFound();
            }

            db.ProductOrders.Remove(productOrder);
            db.SaveChanges();

            return Ok(productOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductOrderExists(int id)
        {
            return db.ProductOrders.Count(e => e.ProductOrderID == id) > 0;
        }
    }
}