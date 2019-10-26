using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee";
            v.ChangesMade = "Viewed Employees Table";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();
            Employee cust = new Employee();
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
            if (id == 0)
            {  
                Employee cust = new Employee();

                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeType").Result;

                cust.EmployeeTypeList = response.Content.ReadAsAsync<List<EmployeeType>>().Result;
                cust.User = new User();
                return View(cust);
            }
            else
            {
                Employee cust = new Employee();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Employees/" + id.ToString()).Result;
                cust = response.Content.ReadAsAsync<Employee>().Result;
                cust.EmployeeTypeList = db.EmployeeTypes.ToList();
                cust.UserName = cust.User.UserEmail;
                cust.Password = cust.User.UserPassword;

                return View(cust);
            }
        }

        [HttpPost]

        public ActionResult AddorEdit(Employee emp, int EmployeeTypeID)
        {
            //AUDIT LOG CODE BELOW
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee";
            v.ChangesMade = "Viewed Employee";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee";
            n.ChangesMade = "Created New Employee";
            n.AuditLogTypeID = 2;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();
            //AUDIT LOG CODE END

            Employee model_ = new Employee();

            model_ = new Employee();
            model_ = emp;
            emp.EmployeeTypeID = EmployeeTypeID;


            model_.User = new User();
            model_.User.UserID = emp.UserID;
            model_.User.UserRoleID = 4;

            HttpResponseMessage response = new HttpResponseMessage();
            if (emp.EmployeeID == 0)
            {
                try
                {
                    model_.User.UserPassword = RandomPassword(7);
                    model_.User.UserEmail = emp.UserName;

                    response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Employees", model_).Result;
                    TempData["SuccessMessage"] = "Saved Successfully";

                    n.DateAccessed = DateTime.Now;
                    n.TableAccessed = "Employee";
                    n.ChangesMade = "Created New Employee";
                    n.AuditLogTypeID = 2;
                    //n.UserID = eqp.user.UserID;
                    db.AuditLogs.Add(n);
                    
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


            }
            else
            {
                response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Employees/" + model_.EmployeeID, model_).Result;

                TempData["SuccessMessage"] = "Updated Successfully";

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Employee";
                n.ChangesMade = "Updated Employee";
                n.AuditLogTypeID = 3;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);

            }
            db.SaveChanges();

            return RedirectToAction("Index");
            
           

            

        }

        public ActionResult Delete(int id)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee";
            v.ChangesMade = "Viewed Employee";
            v.AuditLogTypeID = 1;
            //v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee";
            n.ChangesMade = "Deleted Employee";
            n.AuditLogTypeID = 4;
            //n.UserID = id;
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

        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size and case.   
        // If second parameter is true, the return string is lowercase  
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        // Generate a random password of a given length (optional)  
        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }


    }
}