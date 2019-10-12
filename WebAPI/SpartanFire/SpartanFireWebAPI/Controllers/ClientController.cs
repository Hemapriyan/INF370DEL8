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
    public class ClientController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        // GET: api/Client
        public IQueryable<Client> GetClients()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Clients;
        }

        // GET: api/Client/51
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Client client = db.Clients.Find(id);
            ClientModel model = new ClientModel();
            db.Configuration.ProxyCreationEnabled = false;
            model.emp = db.Clients.Find(id);
            model.user = db.Users.Find(model.emp.UserID);
           

           // model.emp.UserName = model.user.UserEmail;
            //model.emp.Password = model.user.UserPassword;

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Client/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, ClientModel client)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.emp.ClientID)
            {
                return BadRequest();
            }

            db.Entry(client.emp).State = EntityState.Modified;
            db.Entry(client.user).State = EntityState.Modified;
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Client
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                db.Users.Add(client.User);
                db.SaveChanges();
                client.UserID = (db.Users
                            .OrderByDescending(p => p.UserID)
                            .First().UserID);
                db.Clients.Add(client);
                db.SaveChanges();

                

                //db.Clients.Add(client);
                //db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = client.ClientID }, client);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // DELETE: api/Client/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientID == id) > 0;
        }
    }
}