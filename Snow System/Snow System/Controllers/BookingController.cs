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
    { //Code that im pushing . is merge working?
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        static int main = 0;

        //static List<Event> dates;
        //// GET: Booking
        public ActionResult Index(string searchBy,string search)
        {

            return View(db.ServiceRequests.Where(x => x.ServiceRequestDate.ToString().StartsWith(search) || search == null).ToList());
        }
        public ActionResult Index2()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using(SpartanFireDBEntities1 dc = new SpartanFireDBEntities1())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }
        public ActionResult Calendar()
        {
            foreach (ServiceRequest item in db.ServiceRequests.ToList())
            {

                UpdateEvents(item.ServiceRequestID);
            }
            return View();
        }
        //public ActionResult AddDate()
        //{
        //    ServiceRequest dates = db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestID == main);

        //    try
        //    {
        //        Event e = new Event();
        //        e.EventID = dates.ServiceRequestID;
        //        e.Start = dates.ServiceBookedDate;
        //        e.End = dates.ServiceBookedEndDate;
        //        e.Subject = dates.Location.City;
        //        e.IsFullDay = true;
        //        db.Events.Add(e);

        //        db.SaveChanges();
        //    }
        //    catch
        //    {

        //    }
        //    return View("AddService");

        //}
        public ActionResult ServiceRequest(int ?id)
        {
            db.Events.ToList().Clear();
            db.SaveChanges();
            Event obj = new Event();    
            return View();
        }
        public ActionResult BookDate()
        {
            return View("BookDate",db.ServiceRequests.ToList());
        }
      
        public ActionResult AddService(int id=0)
        {
            DateTime T = new DateTime();
            main = id;
            ServiceRequest obj = db.ServiceRequests.ToList().FirstOrDefault(x => x.ServiceRequestID==id);

            if(obj==null)
            {
                return Redirect("http://localhost:7206/ServiceRequest/Index");
            }
           

            foreach (ServiceRequest item in db.ServiceRequests.ToList())
            {
                
                UpdateEvents(item.ServiceRequestID);
            }


            ServiceRequestStatu stat = db.ServiceRequestStatus.ToList().FirstOrDefault(x => x.ServiceRequestStatusID == obj.ServiceRequestStatusID);
            Location loc = db.Locations.ToList().FirstOrDefault(x => x.LocationID == obj.LocationID);

            ViewBag.Location = loc.StreetAddress + ", " + loc.Suburb + ", " + loc.City+", "+loc.PostalCode;
            ViewBag.Comment = obj.Comment;
            ViewBag.status = stat.Description;
            ViewBag.contact=loc.ContactPersonName+", "+loc.ContactPersonNumber;
           
            ViewBag.date = obj.ServiceRequestDate.ToShortDateString();
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ServiceRequest/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<ServiceRequest>().Result);
            
        }
        int startHour;
        int startMin;
        int endHour;
        int endMin;
        [HttpPost]

        public ActionResult AddService(ServiceRequest serve, string startT = "", string endT = "", string Date=null, string eDate = null)
        {
            DateTime startdate=new DateTime();
            DateTime enddate = new DateTime();
            //if(serve.ServiceBookedDate<DateTime.Now || serve.ServiceBookedEndDate<DateTime.Now)
            //{
            //    TempData["ErrorMessage"] = "Date is in Past!";
            //    return RedirectToAction("AddService");
            //}
            if (Date.Length > 0 && eDate.Length>0)
            {
                int month = Convert.ToInt32(Date.Substring(0, 2));
                int day = Convert.ToInt32(Date.Substring(3, 2));
                int year = Convert.ToInt32(Date.Substring(6, 4));
                 startdate = new DateTime(year, month, day);

                int emonth = Convert.ToInt32(eDate.Substring(0, 2));
                int eday = Convert.ToInt32(eDate.Substring(3, 2));
                int eyear = Convert.ToInt32(eDate.Substring(6, 4));
                 enddate = new DateTime(eyear, emonth, eday);

               

            }
            try
            {
                 startHour = Convert.ToInt32(startT.Substring(0, 2));
                 startMin = Convert.ToInt32(startT.Substring(3, 2));
                 endHour = Convert.ToInt32(endT.Substring(0, 2));
                 endMin = Convert.ToInt32(endT.Substring(3, 2));
            }
            catch
            {
                 startHour = 0;
                 startMin = 0;
                 endHour = 0;
                 endMin = 0;

        }
            
            if(startHour<8 || startHour>16)
            {
                ServiceRequest obj1 = db.ServiceRequests.ToList().FirstOrDefault(x => x.ServiceRequestID == main);

                ServiceRequestStatu stat = db.ServiceRequestStatus.ToList().FirstOrDefault(x => x.ServiceRequestStatusID == obj1.ServiceRequestStatusID);
                Location loc = db.Locations.ToList().FirstOrDefault(x => x.LocationID == obj1.LocationID);


               
                ViewBag.Location = loc.StreetAddress + ", " + loc.Suburb + ", " + loc.City + ", " + loc.PostalCode;
                ViewBag.Comment = obj1.Comment;
                ViewBag.status = stat.Description;
                ViewBag.contact = loc.ContactPersonName + ", " + loc.ContactPersonNumber;

                ViewBag.date = obj1.ServiceRequestDate.ToShortDateString();
                TempData["SuccessMessage"] = "Error: Invalid Date/Time input!";
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ServiceRequest/" + main.ToString()).Result;
                return View(response.Content.ReadAsAsync<ServiceRequest>().Result);

            }

            // DateTime obj = new DateTime(year, month, day);
            TimeSpan stime = new TimeSpan(startHour, startMin, 0);
            TimeSpan etime = new TimeSpan(endHour, startMin, 0);

            DateTime sdate = (startdate).Add(stime);
            DateTime edate = (enddate).Add(etime);

            ServiceRequest obj = db.ServiceRequests.ToList().FirstOrDefault(x => x.ServiceRequestID == main);
            obj.ServiceBookedDate = sdate;
            obj.ServiceBookedEndDate = edate;
            if (stime.TotalHours > 0)
                obj.IsFullDay = false;
            else
                obj.IsFullDay = true;
            obj.ServiceRequestStatusID = 2;
            db.SaveChanges();

            return RedirectToAction("Index", "ServiceRequest");
        }


        public void UpdateEvents(int ?id)
        {

            ServiceRequest dates = db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestID == id);

            

            try
            {
                Event e = new Event();
                e.EventID = dates.ServiceRequestID;
                e.Start = dates.ServiceBookedDate;
                e.End = dates.ServiceBookedEndDate;
                e.Subject = dates.Location.ContactPersonName+", "+dates.Location.Suburb;
                e.Description = dates.Comment;
                e.IsFullDay = false;
                db.Events.Add(e);
                db.SaveChanges();
            }
            catch
            {

            }

        }

       
    }
    
}