using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(Snow_System.Models.mvcUserModel userModel)
        {
            

            using (SpartanFireDBEntities1 db =new SpartanFireDBEntities1())
            {
                var userDetails = db.Users.Where(x => x.UserEmail == userModel.UserEmail && x.UserPassword == userModel.UserPassword).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage= "Wrong username or password.";
                    return View("Index", userModel);
                }

                else
                {
                    Session["UserID"] = userDetails.UserID;
                    Session["UserEmail"] = userDetails.UserEmail;
                    Session["UserRoleID"] = userDetails.UserRoleID;
                    int temp =0;
                    if((int)Session["UserRoleID"] == 1)
                    {
                        temp = db.Clients.Where(c => c.UserID == userDetails.UserID).Select(c=>c.ClientID).FirstOrDefault();
                    }
                    Session["ClientID"] = temp;

                    Globals.Username = userModel.UserEmail;
                    Globals.Password = userModel.UserPassword;
                    if (userDetails.UserRoleID == 1)
                    {
                        Location l = db.Locations.Where(m=>m.ClientID == userDetails.UserID).FirstOrDefault();
                        if(l == null)
                        {
                            return RedirectToAction("AddOrEdit", "Location", new { cid = userDetails.UserID }) ;
                        }
                        else
                        {
                            return RedirectToAction("Home", "Home");
                        }
                    }else{
                        return RedirectToAction("Home", "Home");

                        //return RedirectToAction("Index", "Dashboard");
                    }
                }

            }

            
        }

        
        public ActionResult LogOut()
        {
            Globals.Username = "";
            Globals.Password = "";
            int userID = (int)Session["UserID"];
            Session.Abandon();


            return RedirectToAction("Home", "Home");

        }
    }
}