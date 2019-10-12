using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class RegisterClientController : Controller
    {

        // GET: Client
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        public ActionResult Index(string searchBy, string search)
        {
            mvcClientModel cust = new mvcClientModel();
            //cust.ClientTypeList = db.ClientTypes.ToList();
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

            if (id == 0)
            {
                ClientModelGlobal cust = new ClientModelGlobal();
                cust.emp = new Client();
                //cust.ClientTypeList = db.ClientTypes.ToList();
                return View(cust);
            }

            else
            {
                ClientModelGlobal cust = new ClientModelGlobal();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Client/" + id.ToString()).Result;
                cust.emp = new Client();
                //cust.ClientTypeList = db.ClientTypes.ToList();
                cust.emp = response.Content.ReadAsAsync<Client>().Result;
                return View(cust);
            }
        }
        [HttpPost]

        public ActionResult AddorEdit(Client emp, int ClientTypeID)
        {
            ClientModelGlobal model_ = new ClientModelGlobal();
            model_.emp = new Client();
            model_.user = new User();

            model_.emp = emp;
            model_.emp.ClientTypeID = ClientTypeID;

            model_.user.UserID = emp.UserID;
            model_.user.UserPassword = emp.Password;
            model_.user.UserEmail = emp.UserName;
            model_.user.UserRoleID = 1;

            if (emp.ClientID == 0)
            {
                try
                {

                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Client", model_).Result;

                    //db.Clients.Add(emp.c);

                    //db.Users.Add(emp.u);

                    //db.SaveChanges();

                    TempData["SuccessMessage"] = "Saved Successfully";
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
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Client/" + model_.emp.ClientID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index","Login");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Client/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}