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
        // GET: Equipment
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index(string searchBy, string search)
        {

            return View(db.ServiceRequests.Where(x => x.Comment.StartsWith(search) || search == null).ToList());
            //return View();
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new ServiceRequest());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ServiceRequest/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<ServiceRequest>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(ServiceRequest eqp)
        {
            if (eqp.ServiceRequestID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("ServiceRequest", eqp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("ServiceRequest/" + eqp.ServiceRequestID, eqp).Result;
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
    }
}