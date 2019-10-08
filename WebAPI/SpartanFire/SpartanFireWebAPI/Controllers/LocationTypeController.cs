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
    public class LocationTypeController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/LocationType
        public IQueryable<LocationType> GetLocationTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.LocationTypes;
        }

        // GET: api/LocationType/5
        [ResponseType(typeof(LocationType))]
        public IHttpActionResult GetLocationType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            LocationType locationType = db.LocationTypes.Find(id);
            if (locationType == null)
            {
                return NotFound();
            }

            return Ok(locationType);
        }

        // PUT: api/LocationType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocationType(int id, LocationType locationType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationType.LocationTypeID)
            {
                return BadRequest();
            }

            db.Entry(locationType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationTypeExists(id))
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

        // POST: api/LocationType
        [ResponseType(typeof(LocationType))]
        public IHttpActionResult PostLocationType(LocationType locationType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LocationTypes.Add(locationType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = locationType.LocationTypeID }, locationType);
        }

        // DELETE: api/LocationType/5
        [ResponseType(typeof(LocationType))]
        public IHttpActionResult DeleteLocationType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            LocationType locationType = db.LocationTypes.Find(id);
            if (locationType == null)
            {
                return NotFound();
            }

            db.LocationTypes.Remove(locationType);
            db.SaveChanges();

            return Ok(locationType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationTypeExists(int id)
        {
            return db.LocationTypes.Count(e => e.LocationTypeID == id) > 0;
        }
    }
}