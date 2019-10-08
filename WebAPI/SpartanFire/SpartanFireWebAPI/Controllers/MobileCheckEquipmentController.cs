using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SpartanFireWebAPI.Models;

namespace SpartanFireWebAPI.Controllers
{
    public class MobileCheckEquipmentController : ApiController
    {

        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            CheckEquipment checkEquipment = db.CheckEquipments.OrderByDescending(d => d.CheckEquipmentID)
                .ToList().FirstOrDefault();
                
            if (checkEquipment == null)
            {
                return NotFound();
            }

            return Ok(checkEquipment);
        }
    }
}