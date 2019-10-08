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
    public class EmployeeRequestController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/EmployeeRequest
        public IQueryable<EmployeeRequest> GetEmployeeRequests()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.EmployeeRequests;
        }

        // GET: api/EmployeeRequest/5
        [ResponseType(typeof(EmployeeRequest))]
        public IHttpActionResult GetEmployeeRequest(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            EmployeeRequest employeeRequest = db.EmployeeRequests.Find(id);
            if (employeeRequest == null)
            {
                return NotFound();
            }

            return Ok(employeeRequest);
        }

        // PUT: api/EmployeeRequest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeRequest(int id, EmployeeRequest employeeRequest)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeRequest.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employeeRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeRequestExists(id))
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

        // POST: api/EmployeeRequest
        [ResponseType(typeof(EmployeeRequest))]
        public IHttpActionResult PostEmployeeRequest(EmployeeRequest employeeRequest)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeRequests.Add(employeeRequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeRequestExists(employeeRequest.EmployeeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employeeRequest.EmployeeID }, employeeRequest);
        }

        // DELETE: api/EmployeeRequest/5
        [ResponseType(typeof(EmployeeRequest))]
        public IHttpActionResult DeleteEmployeeRequest(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            EmployeeRequest employeeRequest = db.EmployeeRequests.Find(id);
            if (employeeRequest == null)
            {
                return NotFound();
            }

            db.EmployeeRequests.Remove(employeeRequest);
            db.SaveChanges();

            return Ok(employeeRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeRequestExists(int id)
        {
            return db.EmployeeRequests.Count(e => e.EmployeeID == id) > 0;
        }
    }
}