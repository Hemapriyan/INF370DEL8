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
    public class FunctionalityController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/Functionality
        public IQueryable<Functionality> GetFunctionalities()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Functionalities;
        }

        // GET: api/Functionality/5
        [ResponseType(typeof(Functionality))]
        public IHttpActionResult GetFunctionality(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Functionality functionality = db.Functionalities.Find(id);
            if (functionality == null)
            {
                return NotFound();
            }

            return Ok(functionality);
        }

        // PUT: api/Functionality/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFunctionality(int id, Functionality functionality)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != functionality.FunctionalityID)
            {
                return BadRequest();
            }

            db.Entry(functionality).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionalityExists(id))
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

        // POST: api/Functionality
        [ResponseType(typeof(Functionality))]
        public IHttpActionResult PostFunctionality(Functionality functionality)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Functionalities.Add(functionality);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = functionality.FunctionalityID }, functionality);
        }

        // DELETE: api/Functionality/5
        [ResponseType(typeof(Functionality))]
        public IHttpActionResult DeleteFunctionality(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Functionality functionality = db.Functionalities.Find(id);
            if (functionality == null)
            {
                return NotFound();
            }

            db.Functionalities.Remove(functionality);
            db.SaveChanges();

            return Ok(functionality);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FunctionalityExists(int id)
        {
            return db.Functionalities.Count(e => e.FunctionalityID == id) > 0;
        }
    }
}