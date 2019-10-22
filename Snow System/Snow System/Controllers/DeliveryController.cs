using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class DeliveryController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET: Delivery
        public ActionResult Index(string searchBy, string search)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return View(db.Deliveries.Where(x => x.DeliveryStatu.Description.StartsWith(search) || search == null).Include(x=>x.DeliveryStatu).ToList());
        }

        public ActionResult AssignDelivery(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<AssignDelivery> ad = db.AssignDeliveries.Where(d => d.DeliveryID == id).ToList();
            TempData["DeliveryID"] = db.Deliveries.Where(d=>d.DeliveryID == id).Select(s=>s.DeliveryID).FirstOrDefault();
            ViewBag.Status = db.Deliveries.Where(d => d.DeliveryID == id).Select(s => s.DeliveryStatu.Description).FirstOrDefault();
            string address = "";
            address += db.Deliveries.Where(d => d.DeliveryID == id).Select(s => s.ProductOrder.Location.StreetAddress) + ", " + db.Deliveries.Where(d => d.DeliveryID == id).Select(s => s.ProductOrder.Location.Suburb) + ", " + db.Deliveries.Where(d => d.DeliveryID == id).Select(s => s.ProductOrder.Location.City);
            ViewBag.Address = address;
            List<Employee> temp = db.Employees.ToList();
            foreach(AssignDelivery emp in ad)
            {
                foreach(Employee employeetemp in temp)
                {
                    if(emp.EmployeeID == employeetemp.EmployeeID)
                    {
                        temp.Remove(employeetemp);
                    }
                }
            }
            ViewBag.employees = temp;
            return View(ad);
        }

        public ActionResult Assign(int DelID, int employee)
        {
            db.Configuration.ProxyCreationEnabled = false;
            AssignDelivery ad = new AssignDelivery();
            ad.DeliveryID = DelID;
            ad.EmployeeID = employee;
            db.AssignDeliveries.Add(ad);
            db.SaveChanges();
            return RedirectToAction("AssignDelivery", "Delivery", new { id = DelID });
        }
    }
}