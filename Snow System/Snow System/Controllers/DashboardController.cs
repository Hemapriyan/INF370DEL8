using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class DashboardController : Controller
    {

        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET: Dashboards
        public ActionResult Index()
        {
            ViewBag.Deliveries = db.Deliveries.Where(del=>del.DateOfDelivery == DateTime.Today.Date).ToList(); //test
            ViewBag.Services = db.ServiceRequests.Where(srv=>srv.ServiceBookedDate== DateTime.Today.Date).ToList();
            ViewBag.ProductOrders = db.ProductOrders.Where(o => o.ProductOrderStatusID == 2).ToList();
            ViewBag.AllProductOrders = db.ProductOrders.Where(o => o.ProductOrderStatusID > 1 && o.ProductOrderStatusID < 7).ToList();
            ViewBag.SupplierOrders = db.SupplierOrders.Where(o => o.SupplierStatusID == 2 || o.SupplierStatusID == 6).ToList();
            ViewBag.Locations = db.Locations.ToList();
            ViewBag.Ratings = db.ClientFeedbacks.ToList();
            ViewBag.UnassignedDeliveries = db.Deliveries.Where(del => del.DeliveryStatusID == 1).ToList(); // check back
            ViewBag.OutstandingServices = db.ServiceRequests.Where(ser => ser.ServiceRequestStatusID == 1 || ser.ServiceRequestStatusID == 2).ToList();
            return View();
        }

        public ActionResult CancelOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder po = db.ProductOrders.Where(p => p.ProductOrderID == id).FirstOrDefault();
            po.ProductOrderStatusID = 9;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderPayed(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder po = db.ProductOrders.Where(p => p.ProductOrderID == id).FirstOrDefault();
            po.ProductOrderStatusID = 3;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}