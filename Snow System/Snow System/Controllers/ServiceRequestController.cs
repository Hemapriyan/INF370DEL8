using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ServiceRequestController : Controller
    {//Code that im pushing . is merge working?
     // GET: ServiceRequest

        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index(string searchBy, string search)
        {


            //List<Event> events = db.Events.ToList();

            //db.Events.Remove(events[0]);
            //db.SaveChanges();
            try
            {
                foreach (ServiceRequest item in db.ServiceRequests.ToList())
                {
                    Event items = db.Events.Find(item.ServiceRequestID);
                    db.Events.Remove(items);
                    db.SaveChanges();
                }
            }
            catch
            {

            }

            if (searchBy == "Location")
            {
                return View(db.ServiceRequests.Where(x => x.Location.StreetAddress.StartsWith(search) || search == null).ToList());
            }
            else 
            {
                return View(db.ServiceRequests.Where(x => x.ServiceRequestStatu.Description.StartsWith(search) || search == null).ToList());
            }
           // return View(db.ServiceRequests.Where(x => x.ServiceRequestStatu.Description.ToString().StartsWith(search) || search == null).ToList());
            //return View();
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
              
                //ServiceRequest cust = new ServiceRequest();

                //HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Location").Result;

                //cust.ListOfLocations = response.Content.ReadAsAsync<List<Location>>().Result;


                //return View(cust);
                return View(new ServiceRequest());
            }


            else
            {
                ServiceRequest cust = new ServiceRequest();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ServiceRequest/" + id.ToString()).Result;
                cust = response.Content.ReadAsAsync<ServiceRequest>().Result;
                //cust.ListOfLocations = db.Locations.ToList();
                return View(cust);
            }
        }

       

        public ActionResult Makeservicerequest(int id)
        {
            try
            {
                ServiceRequest makeserve = new ServiceRequest();
                makeserve.LocationID = id;

                return View("AddorEdit",makeserve);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public ActionResult AddorEdit(ServiceRequest eqp)
        {
            //CODE FOR DROPDOWNLIST
            ServiceRequest model_ = new ServiceRequest();

            model_ = eqp;
            //eqp.LocationID = ListofLocations;
            //model_.LocationID = eqp.LocationID;
           // model_.Location = new Location();
            //model_.Location.LocationID = eqp.Location.LocationID;
            

            if (eqp.ServiceRequestID == 0)
            {
                model_.ServiceRequestDate = DateTime.Today;
                model_.ServiceRequestStatusID = 1;
                model_.ServiceBookedDate = new DateTime(2000, 1,1 );
                model_.ServiceBookedEndDate = new DateTime(2000, 1, 1);
                model_.IsFullDay = false;
                //db.ServiceRequests.Add(model_);
                //db.SaveChanges();


                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("ServiceRequest", model_).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
                int id = (int)Session["UserRoleID"];
                if(id == 1)
                {
                    return RedirectToAction("Service", "Home");

                }
                else
                {
                    return RedirectToAction("Index");

                }

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("ServiceRequest/" + model_.ServiceRequestID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(int id)
        {
            var temp = db.ServiceRequests.Find(id);

            if(temp.ServiceRequestStatusID == 1 || temp.ServiceRequestStatusID == 2)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("ServiceRequest/" + id.ToString()).Result;
                TempData["SuccessMessage"] = "Deleted Successfully";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["SuccessMessage"] = "Error: Cannot deleted because employee is already assigned.";
            }
            return null;
        }

        public ActionResult LodgeComplaint()
        {
            return View();
        }
    }
}