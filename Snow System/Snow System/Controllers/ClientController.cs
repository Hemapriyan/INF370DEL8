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
                //    Client cust = new Client();

                //    return View(cust);
                Client cust = new Client();


                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("LocationType").Result;

                cust.LocationTypeList = response.Content.ReadAsAsync<List<LocationType>>().Result;
                return View(cust);
            }
            else
            {
                Client cust = new Client();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Client/" + id.ToString()).Result;
  
                cust = response.Content.ReadAsAsync<Client>().Result;
                cust.UserName = cust.User.UserEmail;
                cust.Password = cust.User.UserPassword;
                cust.LocationID = cust.ClientLocation.LocationID;
                cust.StreetAddress = cust.ClientLocation.StreetAddress;
                cust.Suburb = cust.ClientLocation.Suburb;
                cust.City = cust.ClientLocation.City;
                cust.PostalCode = cust.ClientLocation.PostalCode;
                cust.ContactPersonName = cust.ClientLocation.ContactPersonName;
                cust.ContactPersonNumber = cust.ClientLocation.ContactPersonNumber;
                cust.LocationTypeID = cust.ClientLocation.LocationTypeID;
                cust.LocationTypeList = db.LocationTypes.ToList();
                return View(cust);
            }


        }                
            
            
        

        [HttpPost]
        public ActionResult AddorEdit(Client emp,int LocationTypeID)
        {
            //Location loc = new Location();

            Client model_ = new Client();

            model_ = new Client();
            model_ = emp;
            model_.ClientTypeID = 1;
            emp.LocationTypeID = LocationTypeID;

            model_.User = new User();
            model_.User.UserID = emp.UserID;
            model_.User.UserEmail = emp.UserName;
            model_.User.UserRoleID = 1;

            //added by me 11-0ct 17h00 below code. testing of adding location at the same time as adding client

            model_.ClientLocation = new Location();
            model_.ClientLocation.ClientID = emp.ClientID;
            model_.ClientLocation.LocationID = emp.LocationID;
            model_.ClientLocation.StreetAddress = emp.StreetAddress;
            model_.ClientLocation.Suburb = emp.Suburb;
            model_.ClientLocation.City = emp.City;
            model_.ClientLocation.PostalCode = emp.PostalCode;
            model_.ClientLocation.ContactPersonName = emp.ContactPersonName;
            model_.ClientLocation.ContactPersonNumber = emp.ContactPersonNumber;
            model_.ClientLocation.LocationTypeID = emp.LocationTypeID;






            //db.SaveChanges();


            if (emp.ClientID == 0)
            {
                try
                {
                    model_.User.UserPassword = RandomPassword(7);

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