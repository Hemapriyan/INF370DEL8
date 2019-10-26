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

        //
        public ActionResult AddorEdit( int cid, int id = 0 )
        {
            Location cust = new Location();
           


            int action = Convert.ToInt32(Session["Action"]);
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                ViewBag.ActionID = Session["ActionID"];
            }
            else
            {
                ViewBag.ActionID = 2;
            }
            
            if (id == 0)
            {
                Location lcn = new Location();
                lcn.ClientID = Convert.ToInt32(cid);
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("LocationType").Result;
                lcn.LocationTypeList = response.Content.ReadAsAsync<List<LocationType>>().Result;
                return View(lcn);
            }
                
            else
            {
                //HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Location/" + id.ToString()).Result;
                //return View(response.Content.ReadAsAsync<Location>().Result);

                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Location/" + id.ToString()).Result;
                //cust.emp = new Employee();//
                cust = response.Content.ReadAsAsync<Location>().Result;
                cust.LocationTypeList = db.LocationTypes.ToList();

                return View(cust);

            }
        }


        [HttpPost]

        public ActionResult AddorEdit(Location eqp,int LocationTypeID)
        {
            Location model_ = new Location();

            model_ = eqp;
            eqp.LocationTypeID = LocationTypeID;

            int action = Convert.ToInt32(Session["ActionID"]);
            if (action == 0)
            {
                Session["ActionID"] = 3;
            }
            //eqp.LocationTypeID = 1;
            if (eqp.LocationID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Location", model_).Result;
                TempData["SuccessMessage"] = "Saved Successfully";

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Location/" + model_.LocationID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("GoBack");
        }

        public ActionResult Delete(int id)
        {
            int ClientID = db.Locations.Where(loc => loc.LocationID == id).Select(loc=>loc.ClientID).FirstOrDefault();
            List<Location> cLocations = db.Locations.Where(x => x.ClientID == ClientID).ToList();
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Location/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("GoBack");
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
            else if (searchBy == "ClientID")
            {
                int cid = Convert.ToInt32(search);
                ViewBag.Client = Convert.ToInt32(search);
                string cn = db.Clients.Where(c => c.ClientID == cid).Select(c => c.ClientName).FirstOrDefault() + " " + db.Clients.Where(c => c.ClientID == cid).Select(c => c.ClientSurname).FirstOrDefault() ;
                ViewBag.ClientName = cn;
                Session["ClientID"] = cid;
                return View(db.Locations.Where(x => x.ClientID == cid).ToList());
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

        public ActionResult ChooseLocationDelivery(int id)//Takes in Product Order ID
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder po = db.ProductOrders.Where(p => p.ProductOrderID == id).FirstOrDefault();
            List<Location> l = po.Location.Client.Locations.ToList();
            Session["ActionID"] = 3;
            Session["OrderID"] = id;
            return View(l);
        }

        public ActionResult ChooseLocationService(int id)// takes in service ID
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequest po = db.ServiceRequests.Where(p => p.ServiceRequestID == id).FirstOrDefault();
            List<Location> l = po.Location.Client.Locations.ToList();
            Session["ActionID"] = 4;
            Session["ServeID"] = id;
            return View(l);
        }

        public ActionResult GoBack()
        {
            if (Convert.ToInt32(Session["UserRoleID"]) == 1)
            {
                if (Convert.ToInt32(Session["ActionID"]) == 1)
                {
                    return RedirectToAction("Home", "Home");
                }
                if (Convert.ToInt32(Session["ActionID"]) == 2)
                {
                    return RedirectToAction("Locations");
                }
                else if (Convert.ToInt32(Session["ActionID"]) == 3)
                {
                    return RedirectToAction("ChooseLocationDelivery" , new { id  = Convert.ToInt32(Session["OrderID"]) });
                }
                else
                {
                    return RedirectToAction("ChooseLocationService", new {id = Convert.ToInt32(Session["ServiceID"]) });
                }
            }
            else
            {
                if (Convert.ToInt32(Session["ActionID"]) == 1)
                {
                    return RedirectToAction("EmployeeChooseOrderLocation", new { id = Convert.ToInt32(Session["OrderID"]) });
                }
                else if (Convert.ToInt32(Session["ActionID"]) == 2)
                {
                    return RedirectToAction("EmployeeChooseServiceLocation", new { id = Convert.ToInt32(Session["ServiceID"]) });
                }
                else if (Convert.ToInt32(Session["ActionID"]) == 3)
                {
                    int s = Convert.ToInt32(Session["ClientID"]);
                    return RedirectToAction("Index", "Location" , new { searchBy = "ClientID", search = s });
                }
                else
                {
                    return RedirectToAction("Index", "Client");
                }
            }
        }

        public ActionResult EmployeeChooseOrderLocation(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder po = db.ProductOrders.Where(p => p.ProductOrderID == id).FirstOrDefault();
            List<Location> l = po.Location.Client.Locations.ToList();
            Session["ActionID"] = 1;
            Session["OrderID"] = id;
            return View(l);
        }

        public ActionResult EmployeeChooseServiceLocation(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ServiceRequest po = db.ServiceRequests.Where(p => p.ServiceRequestID == id).FirstOrDefault();
            List<Location> l = po.Location.Client.Locations.ToList();
            Session["ActionID"] = 2;
            Session["ServeID"] = id;
            return View(l);
        }

        public ActionResult Locations()
        {
            int clientID = Convert.ToInt32(Session["ClientID"]);
            List<Location> ClientLocations = db.Locations.Where(l => l.ClientID == clientID).ToList();
            return View(ClientLocations);
        }
    }
}