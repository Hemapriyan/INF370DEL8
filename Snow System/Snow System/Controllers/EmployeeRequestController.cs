using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class EmployeeRequestController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        AuditLog n = new AuditLog();
        static int ServiceRequestID = 0;
        // GET: EmployeeRequest
        public ActionResult Index()
        {
            return View(db.EmployeeRequests.ToList());
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                id = ServiceRequestID;
            }
            else
            {
                ServiceRequestID = id;
            }
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeRequest/" + ServiceRequestID.ToString()).Result;

            EmployeeRequest model = new EmployeeRequest();
            model.Employee = new Employee();
            model.EmployeeRequestList = response.Content.ReadAsAsync<List<EmployeeRequest>>().Result;

            var DateBooked = db.ServiceRequests.ToList().FirstOrDefault(x => x.ServiceRequestID == ServiceRequestID).ServiceBookedDate;

            //var BookedAlready = (from a in db.Employees
            //                     join b in db.EmployeeRequests.DefaultIfEmpty()
            //                     on a.EmployeeID equals b.EmployeeID
            //                     join c in db.ServiceRequests.Where(x => x.ServiceRequestStatusID == 3 &&
            //                     DateTime.Compare(x.ServiceBookedDate, DateBooked) != 0).DefaultIfEmpty()
            //                     on b.ServiceRequestID equals c.ServiceRequestID
            //                     select new
            //                     {
            //                         a.EmployeeID
            //                     }).ToList();

            foreach (var item in model.EmployeeRequestList)
            {
                item.Employee = db.Employees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
            }
            //ViewBag.Status = db.ServiceRequestStatus.Where(d => d.ServiceRequestStatusID == id).Select(s => s.Description).FirstOrDefault();
            model.EmployeeList = new List<Employee>();
            //foreach(var item in BookedAlready)
            //{
            //    var a = new Employee();
            //    a = db.Employees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
            //    model.EmployeeList.Add(a);
            //}
            var tempList = db.Employees.Where(x => x.EmployeeTypeID == 2).ToList();
            foreach(var item in tempList)
            {
                var exists = (from a in db.Employees.Where(x => x.EmployeeID == item.EmployeeID)
                                     join b in db.EmployeeRequests
                                     on a.EmployeeID equals b.EmployeeID
                                     join c in db.ServiceRequests.Where(x => x.ServiceRequestStatusID == 3 &&
                                     DateTime.Compare(x.ServiceBookedDate, DateBooked) == 0)
                                     on b.ServiceRequestID equals c.ServiceRequestID
                                     select new
                                     {
                                         a.EmployeeID
                                     }).ToList();
                if(exists.Count() == 0)
                {
                    model.EmployeeList.Add(item);
                }
            }
            model.DateAssigned = DateTime.Today;
            ServiceRequest obj = db.ServiceRequests.ToList().FirstOrDefault(x => x.ServiceRequestID == ServiceRequestID);
            ServiceRequestStatu stat = db.ServiceRequestStatus.ToList().FirstOrDefault(x => x.ServiceRequestStatusID == obj.ServiceRequestStatusID);

            Location loc = db.Locations.ToList().FirstOrDefault(x => x.LocationID == obj.LocationID);

            ViewBag.status = stat.Description;
            ViewBag.Location = loc.StreetAddress + ", " + loc.Suburb + ", " + loc.City + ", " + loc.PostalCode;
            ViewBag.date = obj.ServiceBookedDate.ToShortDateString();

            model.ServiceRequestID = ServiceRequestID;

            return View(model);
        }

        [HttpPost]

        public ActionResult AddorEdit(EmployeeRequest model, int EmployeeID)
        {

            model.EmployeeID = EmployeeID;
            try
            {
                var exists = db.EmployeeRequests.Where(x => x.EmployeeID == EmployeeID & x.ServiceRequestID == model.ServiceRequestID);
                if(exists.Count() == 0)
                {
                    var DateBooked = db.ServiceRequests.ToList().FirstOrDefault(x => x.ServiceRequestID == ServiceRequestID).ServiceBookedDate;

                    var BookedAlready = (from a in db.ServiceRequests.Where(x => x.ServiceBookedDate == DateBooked)
                                         join b in db.EmployeeRequests.Where(x => x.EmployeeID == EmployeeID)
                                        on a.ServiceRequestID equals b.ServiceRequestID
                                        select new
                                        {
                                            a.ServiceRequestID
                                        }).ToList();

                    if(BookedAlready.Count() == 0)
                    {
                        HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("EmployeeRequest", model).Result;

                        TempData["SuccessMessage"] = "Saved Successfully";

                        n.DateAccessed = DateTime.Now;
                        n.TableAccessed = "Client";
                        n.ChangesMade = "Created New Client";
                        n.AuditLogTypeID = 2;
                        n.UserID = model.UserID;
                        db.AuditLogs.Add(n);
                        db.SaveChanges();

                        return RedirectToAction("AddorEdit", "EmployeeRequest");
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Error: Employee already booked for the day";
                    }
                }
                else
                {
                    TempData["SuccessMessage"] = "Error: Exists already";
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            model.EmployeeRequestList = db.EmployeeRequests.Where(x => x.ServiceRequestID == model.ServiceRequestID).ToList();
            model.Employee = new Employee();
            foreach (var item in model.EmployeeRequestList)
            {
                item.Employee = db.Employees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
            }
            model.EmployeeList = db.Employees.Where(x => x.EmployeeTypeID == 2).ToList();
            return View(model);
        }

        public ActionResult Delete(int EmployeeID, int ServiceRequestId)
        {
            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee Request";
            n.ChangesMade = "Unassigned employee";
            n.AuditLogTypeID = 4;
            n.UserID = EmployeeID;
            db.AuditLogs.Add(n);

            EmployeeRequest employeeRequest = db.EmployeeRequests.Where(x => x.EmployeeID == EmployeeID & x.ServiceRequestID == ServiceRequestId).FirstOrDefault();

            db.EmployeeRequests.Remove(employeeRequest);

            db.SaveChanges();

            //HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("EmployeeRequest/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            var EmployeeRequests = db.EmployeeRequests.Where(x => x.ServiceRequestID == ServiceRequestId).ToList();
            if(EmployeeRequests.Count() == 0)
            {
                var temp = db.ServiceRequests.Find(employeeRequest.ServiceRequestID);
                temp.ServiceRequestStatusID = 2;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }

            ServiceRequestID = ServiceRequestId;
            return RedirectToAction("AddorEdit", "EmployeeRequest");
        }
    }
}