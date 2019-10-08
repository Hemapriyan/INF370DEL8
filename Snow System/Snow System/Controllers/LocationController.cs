using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class LocationController : Controller
    {
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        //public ActionResult Index(string searchBy, string search)
        //{

        //    if (searchBy == "Suburb")
        //    {
        //        return View(db.Locations.Where(x => x.Suburb.StartsWith(search) || search == null).ToList());
        //    }
        //    else if (searchBy == "City")
        //    {
        //        return View(db.Locations.Where(x => x.City.StartsWith(search) || search == null).ToList());
        //    }
        //    else
        //    {
        //        return View(db.Locations.Where(x => x.ContactPersonNumber.StartsWith(search) || search == null).ToList());

        //    }

        //}

        public ActionResult AddorEdit( int cid, int id = 0 )
        {
            int action = Convert.ToInt32(TempData["ActionID"]);
            ViewBag.ActionID = TempData["ActionID"];
            if (id == 0)
            {
                Location lcn = new Location();
                lcn.ClientID = Convert.ToInt32(cid);
                return View(lcn);
            }
                
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Location/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Location>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(Location eqp)
        {

            eqp.LocationTypeID = 1;
            if (eqp.LocationID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Location", eqp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Location/" + eqp.LocationID, eqp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Location/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
        // GET: Location
        public ActionResult Index(string searchBy, string search)
        {
            mvcLocationModel cust = new mvcLocationModel();
            cust.LocationTypeList = db.LocationTypes.ToList();
            if (searchBy == "Suburb")
            {
                return View(db.Locations.Where(x => x.Suburb.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "City")
            {
                return View(db.Locations.Where(x => x.City.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Locations.Where(x => x.ContactPersonNumber.StartsWith(search) || search == null).ToList());

            }

        }
        //public ActionResult AddorEdit(int id = 0, int cid)
        //{
        //    int action = Convert.ToInt32(TempData["ActionID"]);
        //    ViewBag.Action = Convert.ToInt32(TempData["ActionID"]);
        //    if (id == 0)
        //    {
        //        LocationModelGlobal cust = new LocationModelGlobal();
        //        cust.emp = new mvcLocationModel();
        //        cust.client
        //        cust.LocationTypeList = db.LocationTypes.ToList();
        //        return View(cust);
        //    }
        //    else
        //    {
        //        LocationModelGlobal cust = new LocationModelGlobal();
        //        HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Location/" + id.ToString()).Result;
        //        cust.emp = new mvcLocationModel();
        //        cust.LocationTypeList = db.LocationTypes.ToList();
        //        cust.emp = response.Content.ReadAsAsync<mvcLocationModel>().Result;
        //        return View(cust);
        //    }
        //}


        //[HttpPost]

        //public ActionResult AddorEdit(mvcLocationModel emp, int LocationTypeID)
        //{
        //    LocationModelGlobal model_ = new LocationModelGlobal();
        //    ViewBag.ActionID = TempData["ActionID"];
        //    model_.emp = new mvcLocationModel();
        //    model_.emp = emp;
        //    emp.LocationTypeID = LocationTypeID;

        //    model_.client = new Client();

        //    //model_.client.ClientName = emp.Client.ClientName;
        //    //model_.client.ClientSurname = emp.Client.ClientSurname;
        //    //model_.client.ContactNumber = emp.Client.ContactNumber;
        //    //model_.client.EmailAddress = emp.Client.EmailAddress;
        //    //model_.client.HouseAddress = emp.Client.HouseAddress;
        //    //model_.client.ClientTypeID = emp.Client.ClientTypeID;
        //    //model_.client.UserID = emp.Client.UserID;
        //    if (emp.LocationID == 0)
        //    {
        //        HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Location", model_).Result;
        //        TempData["SuccessMessage"] = "Saved Successfully";


        //    }
        //    else
        //    {
        //        HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Location/" + emp.LocationID, emp).Result;
        //        TempData["SuccessMessage"] = "Updated Successfully";
        //    }
        //    return RedirectToAction("Index");

        //}

        //public ActionResult Delete(int id)
        //{
        //    HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Location/" + id.ToString()).Result;
        //    TempData["SuccessMessage"] = "Deleted Successfully";

        //    return RedirectToAction("Index");

        //}

        public ActionResult ChooseLocationDelivery(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder po = db.ProductOrders.Where(p => p.ProductOrderID == id).FirstOrDefault();
            List<Location> l = po.Location.Client.Locations.ToList();
            TempData["ActionID"] = 1;
            TempData["OrderID"] = id;
            return View(l);
        }

        public ActionResult ChooseLocationService(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequest po = db.ServiceRequests.Where(p => p.ServiceRequestID == id).FirstOrDefault();
            List<Location> l = po.Location.Client.Locations.ToList();
            TempData["ActionID"] = 2;
            TempData["ServeID"] = id;
            return View(l);
        }
    }
}