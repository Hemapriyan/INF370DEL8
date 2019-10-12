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
    public class BankingDetailController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/BankingDetail
        public IQueryable<BankingDetail> GetBankingDetails()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.BankingDetails;
        }

        // GET: api/BankingDetail/5
        [ResponseType(typeof(BankingDetail))]
        public IHttpActionResult GetBankingDetail(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            BankingDetail bankingDetail = db.BankingDetails.Find(id);
            if (bankingDetail == null)
            {
                return NotFound();
            }

            return Ok(bankingDetail);
        }

        // PUT: api/BankingDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBankingDetail(int id, BankingDetail bankingDetail)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankingDetail.BankingID)
            {
                return BadRequest();
            }

            db.Entry(bankingDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankingDetailExists(id))
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

        // POST: api/BankingDetail
        [ResponseType(typeof(BankingDetail))]
        public IHttpActionResult PostBankingDetail(BankingDetail bankingDetail)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BankingDetails.Add(bankingDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bankingDetail.BankingID }, bankingDetail);
        }

        // DELETE: api/BankingDetail/5
        [ResponseType(typeof(BankingDetail))]
        public IHttpActionResult DeleteBankingDetail(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            BankingDetail bankingDetail = db.BankingDetails.Find(id);
            if (bankingDetail == null)
            {
                return NotFound();
            }

            db.BankingDetails.Remove(bankingDetail);
            db.SaveChanges();

            return Ok(bankingDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankingDetailExists(int id)
        {
            return db.BankingDetails.Count(e => e.BankingID == id) > 0;
        }
    }
}