using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            IEnumerable<mvcServiceRequestModel> clientList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ServiceRequest").Result;

            clientList = response.Content.ReadAsAsync<IEnumerable<mvcServiceRequestModel>>().Result;

            return View(clientList);
        }

        public ActionResult AddOrEdit(int id=0)
        {
            if (id==0)
            return View(new mvcServiceRequestModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ServiceRequest/"+ id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcServiceRequestModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(mvcServiceRequestModel serv)
        {
            if (serv.ServiceRequestID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("ServiceRequest", serv).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("ServiceRequest/"+serv.ServiceRequestID, serv).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult AssignService()
        {
            IEnumerable<mvcEmployeeModel> clientList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Employees").Result;

            clientList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

            return View(clientList);
            
        }
    }
}