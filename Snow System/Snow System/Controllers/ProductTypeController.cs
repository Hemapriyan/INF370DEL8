using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ProductTypeController : Controller
    {
        // GET: ProductType

        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult Index(string searchBy, string search)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Product Type";
            v.ChangesMade = "Viewed Product Types Table";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();
            return View(db.ProductTypes.Where(x => x.Description.StartsWith(search)|| search == null).ToList());


        }


        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new ProductType());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ProductType/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<ProductType>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(ProductType prod)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Product Type";
            v.ChangesMade = "Viewed Product Type";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Product Type";
            n.ChangesMade = "Created New Product Type";
            n.AuditLogTypeID = 2;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();

            if (prod.ProductTypeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("ProductType", prod).Result;
                v.DateAccessed = DateTime.Now;
                v.TableAccessed = "Product Type";
                v.ChangesMade = "Viewed Product Type";
                v.AuditLogTypeID = 1;
                //v.UserID = eqp.user.UserID;
                db.AuditLogs.Add(v);

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Product Type";
                n.ChangesMade = "Created New Product Type";
                n.AuditLogTypeID = 2;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("ProductType/" + prod.ProductTypeID, prod).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Product Type";
                n.ChangesMade = "Updated Product Type";
                n.AuditLogTypeID = 3;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("ProductType/" + id.ToString()).Result;
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Product Type";
            v.ChangesMade = "Viewed Product Type";
            v.AuditLogTypeID = 1;
            //v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Product Type";
            n.ChangesMade = "Deleted Product Type";
            n.AuditLogTypeID = 4;
            //n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}