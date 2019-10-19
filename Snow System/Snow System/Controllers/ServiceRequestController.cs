using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ServiceRequestController : Controller
    {
        // GET: ServiceRequest
        
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index(string searchBy, string search)
        {

            return View(db.ServiceRequests.Where(x => x.ServiceRequestStatu.Description.ToString().StartsWith(search) || search == null).ToList());
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
            
            model_.ServiceRequestDate = DateTime.Today;
            model_.ServiceRequestStatusID = 1;

            if (eqp.ServiceRequestID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("ServiceRequest", model_).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("ServiceRequest/" + model_.ServiceRequestID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("ServiceRequest/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }

        public ActionResult LodgeComplaint()
        {
            return View();
        }
    }
}