using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace Snow_System.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employee
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        
        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "ID Number")
            {
                return View(db.Employees.Where(x => x.IDNumber.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Employees.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
        }

        public ActionResult AddorEdit(int id = 0)
        {
            //ViewBag.EmployeeType = new SelectList(db.EmployeeTypes, "EmployeeTypeID", "Description");
     
            
            if (id == 0)
            {
                EmployeeModelGlobal cust = new EmployeeModelGlobal();
                cust.emp = new mvcEmployeeModel();

                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeType").Result;

                cust.EmployeeTypeList = response.Content.ReadAsAsync<List<EmployeeType>>().Result;
                return View(cust);
            }
            else
            {
                EmployeeModelGlobal cust = new EmployeeModelGlobal();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Employees/" + id.ToString()).Result;
                cust.emp = new mvcEmployeeModel();
                cust.EmployeeTypeList = db.EmployeeTypes.ToList();
                cust.emp = response.Content.ReadAsAsync<EmployeeModelGlobal>().Result.emp;
                cust.emp.UserName = cust.emp.UserName;
                cust.emp.Password = cust.emp.Password;

                return View(cust);
            }
        }

        [HttpPost]

        public ActionResult AddorEdit(mvcEmployeeModel emp,int EmployeeTypeID)
        {
            EmployeeModelGlobal model_ = new EmployeeModelGlobal();
            model_.emp = new mvcEmployeeModel();
            model_.emp = emp;
            emp.EmployeeTypeID = EmployeeTypeID;

            model_.user = new User();
            model_.user.UserID = emp.UserID;
            model_.user.UserPassword = emp.Password;
            model_.user.UserEmail = emp.UserName;
            model_.user.UserRoleID = 4;

            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee";
            v.ChangesMade = "Viewed Employee";
            v.AuditLogTypeID = 1;
            v.UserID = emp.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee";
            n.ChangesMade = "Created New Employeee";
            n.AuditLogTypeID = 2;
            n.UserID = emp.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();

            HttpResponseMessage response = new HttpResponseMessage();
            if (emp.EmployeeID == 0)
            {
                response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Employees", model_).Result;
                v.DateAccessed = DateTime.Now;
                v.TableAccessed = "Employee";
                v.ChangesMade = "Viewed Employee";
                v.AuditLogTypeID = 1;
                v.UserID = emp.UserID;
                db.AuditLogs.Add(v);

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Employee";
                n.ChangesMade = "Created New Employeee";
                n.AuditLogTypeID = 2;
                n.UserID = emp.UserID;
                db.AuditLogs.Add(n);
                db.SaveChanges();

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Saved Successfully";

                   
                }
                else
                {
                    TempData["SuccessMessage"] = "Error Occured";
                }
            }
            else
            {
                response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Employees/" + model_.emp.EmployeeID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Employee";
                n.ChangesMade = "Updated Employee";
                n.AuditLogTypeID = 3;
                n.UserID = emp.User.UserID;
                db.AuditLogs.Add(n);

            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddorEdit");
            }

            

        }

        public ActionResult Delete(int id)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee";
            v.ChangesMade = "Viewed Employee";
            v.AuditLogTypeID = 1;
            v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee";
            n.ChangesMade = "Deleted Employee";
            n.AuditLogTypeID = 4;
            n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();

            try
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Employees/" + id.ToString()).Result;
                TempData["SuccessMessage"] = "Deleted Successfully";

                
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
                ViewBag.Status = " Not Deleted Successfully.";
            }
            return RedirectToAction("Index");

        }
    }
}