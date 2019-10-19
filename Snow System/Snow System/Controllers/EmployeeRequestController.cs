using System;
using System.Collections.Generic;
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
        int ServiceRequestID;
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
            foreach(var item in model.EmployeeRequestList)
            {
                item.Employee = db.Employees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
            }
            //ViewBag.Status = db.ServiceRequestStatus.Where(d => d.ServiceRequestStatusID == id).Select(s => s.Description).FirstOrDefault();

            model.EmployeeList = model.EmployeeList = db.Employees.Where(x => x.EmployeeTypeID == 2).ToList();
            model.DateAssigned = DateTime.Today;


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
            TempData["ErrorMessage"] = "Error: Exists already";
            model.EmployeeRequestList = db.EmployeeRequests.Where(x => x.ServiceRequestID == model.ServiceRequestID).ToList();
            model.Employee = new Employee();
            foreach (var item in model.EmployeeRequestList)
            {
                item.Employee = db.Employees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
            }
            model.EmployeeList = db.Employees.Where(x => x.EmployeeTypeID == 2).ToList();
            return View(model);
        }

        public ActionResult Delete(int EmployeeID, int ServiceRequestID)
        {
            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee Request";
            n.ChangesMade = "Unassigned employee";
            n.AuditLogTypeID = 4;
            n.UserID = EmployeeID;
            db.AuditLogs.Add(n);

            EmployeeRequest employeeRequest = db.EmployeeRequests.Where(x => x.EmployeeID == EmployeeID & x.ServiceRequestID == ServiceRequestID).FirstOrDefault();

            db.EmployeeRequests.Remove(employeeRequest);

            db.SaveChanges();

            //HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("EmployeeRequest/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";


            return RedirectToAction("AddorEdit", "EmployeeRequest");
        }
    }
}