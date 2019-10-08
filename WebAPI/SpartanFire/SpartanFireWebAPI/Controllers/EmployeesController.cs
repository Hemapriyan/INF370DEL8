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
    public class EmployeesController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            db.Configuration.ProxyCreationEnabled = false;
            
            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            EmployeeModelGlobal model = new EmployeeModelGlobal();
            db.Configuration.ProxyCreationEnabled = false;
            model.emp = db.Employees.Find(id);
            model.user = db.Users.Find(model.emp.UserID);

            //model.emp.UserName = model.user.UserEmail;
            //model.emp.Password = model.user.UserPassword;

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, EmployeeModelGlobal employee)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.emp.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee.emp).State = EntityState.Modified;
            db.Entry(employee.user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(EmployeeModelGlobal employee)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Users.Add(employee.user);
                db.SaveChanges();
                employee.emp.UserID = (db.Users
                            .OrderByDescending(p => p.UserID)
                            .First().UserID);
                db.Employees.Add(employee.emp);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = employee.emp.EmployeeID }, employee);
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}