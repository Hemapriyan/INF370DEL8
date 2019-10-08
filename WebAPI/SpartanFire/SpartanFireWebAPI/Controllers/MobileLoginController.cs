using SpartanFireWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;

namespace SpartanFireWebAPI.Controllers
{
    public class MobileLoginController : ApiController
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET api/<controller>/5
        [ResponseType(typeof(User))]
        public IEnumerable<User> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Users.Where(u => u.UserRoleID == 2).ToList();
        }
    }
}