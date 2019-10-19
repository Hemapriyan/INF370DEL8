using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class BookingController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        static List<ServiceRequest> dates;
        // GET: Booking
        public ActionResult Index(string searchBy,string search)
        {
            return View(db.ServiceRequests.Where(x => x.ServiceRequestDate.ToString().StartsWith(search) || search == null).ToList());


        }
        public JsonResult getDates()
        {
            using(SpartanFireDBEntities1 dc = new SpartanFireDBEntities1())
            {
                 dates = dc.ServiceRequests.ToList();
                return new JsonResult { Data = dates, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }
        public ActionResult ServiceRequest()
        {
            return View(db.ServiceRequests.ToList());
        }
        public ActionResult BookDate()
        {
            return View("BookDate",db.ServiceRequests.ToList());
        }
    }
    
}