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
    public class CompanyInfoController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/CompanyInfo
        public IQueryable<CompanyInfo> GetCompanyInfoes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CompanyInfoes;
        }

        // GET: api/CompanyInfo/5
        [ResponseType(typeof(CompanyInfo))]
        public IHttpActionResult GetCompanyInfo(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CompanyInfo companyInfo = db.CompanyInfoes.Find(id);
            if (companyInfo == null)
            {
                return NotFound();
            }

            return Ok(companyInfo);
        }

        // PUT: api/CompanyInfo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyInfo(int id, CompanyInfo companyInfo)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyInfo.CompanyID)
            {
                return BadRequest();
            }

            db.Entry(companyInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyInfoExists(id))
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

        // POST: api/CompanyInfo
        [ResponseType(typeof(CompanyInfo))]
        public IHttpActionResult PostCompanyInfo(CompanyInfo companyInfo)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompanyInfoes.Add(companyInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyInfo.CompanyID }, companyInfo);
        }

        // DELETE: api/CompanyInfo/5
        [ResponseType(typeof(CompanyInfo))]
        public IHttpActionResult DeleteCompanyInfo(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CompanyInfo companyInfo = db.CompanyInfoes.Find(id);
            if (companyInfo == null)
            {
                return NotFound();
            }

            db.CompanyInfoes.Remove(companyInfo);
            db.SaveChanges();

            return Ok(companyInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyInfoExists(int id)
        {
            return db.CompanyInfoes.Count(e => e.CompanyID == id) > 0;
        }
    }
}