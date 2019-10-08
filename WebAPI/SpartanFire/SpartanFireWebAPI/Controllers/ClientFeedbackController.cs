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
    public class ClientFeedbackController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/ClientFeedback
        public IQueryable<ClientFeedback> GetClientFeedbacks()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ClientFeedbacks;
        }

        // GET: api/ClientFeedback/5
        [ResponseType(typeof(ClientFeedback))]
        public IHttpActionResult GetClientFeedback(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ClientFeedback clientFeedback = db.ClientFeedbacks.Find(id);
            if (clientFeedback == null)
            {
                return NotFound();
            }

            return Ok(clientFeedback);
        }

        // PUT: api/ClientFeedback/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClientFeedback(int id, ClientFeedback clientFeedback)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientFeedback.ClientFeedBackID)
            {
                return BadRequest();
            }

            db.Entry(clientFeedback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientFeedbackExists(id))
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

        // POST: api/ClientFeedback
        [ResponseType(typeof(ClientFeedback))]
        public IHttpActionResult PostClientFeedback(ClientFeedback clientFeedback)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientFeedbacks.Add(clientFeedback);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clientFeedback.ClientFeedBackID }, clientFeedback);
        }

        // DELETE: api/ClientFeedback/5
        [ResponseType(typeof(ClientFeedback))]
        public IHttpActionResult DeleteClientFeedback(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ClientFeedback clientFeedback = db.ClientFeedbacks.Find(id);
            if (clientFeedback == null)
            {
                return NotFound();
            }

            db.ClientFeedbacks.Remove(clientFeedback);
            db.SaveChanges();

            return Ok(clientFeedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientFeedbackExists(int id)
        {
            return db.ClientFeedbacks.Count(e => e.ClientFeedBackID == id) > 0;
        }
    }
}