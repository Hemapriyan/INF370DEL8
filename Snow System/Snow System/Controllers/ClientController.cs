using Snow_System.CustomFilters;
using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
   
    public class ClientController : Controller
    {
       
        // GET: Client
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();

        public ActionResult Index(string searchBy, string search)
        {
            //int d=ConvertTo...
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Client";
            v.ChangesMade = "Viewed Clients Table";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();
            Client cust = new Client();
            
            if (searchBy == "Contact Number")
            {
                return View(db.Clients.Where(x => x.ContactNumber == search || search == null).ToList());
            }
            else
            {
                return View(db.Clients.Where(x => x.ClientName.StartsWith(search) || search == null).ToList());
            }
        }

        public ActionResult AddorEdit(int id = 0)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Client";
            v.ChangesMade = "Viewed Client";
            v.AuditLogTypeID = 1;
            v.UserID = id;
            db.AuditLogs.Add(v);
            db.SaveChanges();

            if (id == 0)
            {
                Client cust = new Client();
               // cust.emp = new Client();
                
                return View(cust);
            }                
            
            else
            {
                Client cust = new Client();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Client/" + id.ToString()).Result;
                
                
                cust = response.Content.ReadAsAsync<Client>().Result;
                return View(cust);
            }
        }
        [HttpPost]

        public ActionResult AddorEdit(Client emp)
        {
            //Location loc = new Location();

            Client model_ = new Client();

            model_ = new Client();
            model_ = emp;
            model_.ClientTypeID = 1;

            model_.User = new User();
            model_.User.UserPassword = RandomPassword(7);
            model_.User.UserID = emp.UserID;
            model_.User.UserEmail = emp.UserName;
            model_.User.UserRoleID = 1;

            //added by me 11-0ct 17h00 below code. testing of adding location at the same time as adding client

            //model_.location = new Location();
            //model_.location.LocationID = emp.LocationID;
            //model_.location.StreetAddress = emp.StreetAddress;
            //model_.location.Suburb = emp.Suburb;
            //model_.location.City = emp.City;
            //model_.location.PostalCode = emp.PostalCode;
            //model_.location.ContactPersonName = emp.ContactPersonName;
            //model_.location.ContactPersonNumber = emp.ContactPersonNumber;
            //model_.location.LocationTypeID = emp.LocationTypeID;

            




            //db.SaveChanges();


            if (emp.ClientID == 0)
            {
                try
                {

                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Client", model_).Result;
                   
                    TempData["SuccessMessage"] = "Saved Successfully";

                    n.DateAccessed = DateTime.Now;
                    n.TableAccessed = "Client";
                    n.ChangesMade = "Created New Client";
                    n.AuditLogTypeID = 2;
                    n.UserID = emp.UserID;
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
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Client/"+ model_.ClientID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Client";
                n.ChangesMade = "Updated Client";
                n.AuditLogTypeID = 3;
                n.UserID = emp.User.UserID;
                db.AuditLogs.Add(n);

                
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            //v.AuditLogID += 1;
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Client";
            v.ChangesMade = "Viewed Client";
            v.AuditLogTypeID = 1;
            v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Client";
            n.ChangesMade = "Deleted Client";
            n.AuditLogTypeID = 4;
            n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();

            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Client/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
                       

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