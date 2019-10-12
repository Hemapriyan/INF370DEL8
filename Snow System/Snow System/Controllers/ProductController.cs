using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Snow_System.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult Index(string searchBy, string search)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Product";
            v.ChangesMade = "Viewed Products Table";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();
            if (searchBy == "Description")
            {
                return View(db.Products.Where(x => x.ProductDescription.StartsWith(search) || search == null).ToList());
            }
            else if(searchBy=="Name")
            {
                return View(db.Products.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Products.Where(x => x.ProductStatu.Description.StartsWith(search) || search == null).ToList());

            }
        }


        public ActionResult AddorEdit(int id = 0)
        {
            
            if (id == 0)
            {
                Product cust = new Product();
                

                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ProductType").Result;
                HttpResponseMessage response2 = GlobalVariables.WebAPIClient.GetAsync("ProductStatus").Result;

                cust.ProductTypeList = response.Content.ReadAsAsync<List<ProductType>>().Result;
                cust.ProductStatusList = response2.Content.ReadAsAsync<List<ProductStatu>>().Result;//
                return View(cust);
            }
            //return View(new Product());
            else
            {
                //HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;
                //return View(response.Content.ReadAsAsync<Product>().Result);
                Product cust = new Product();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;
                //cust.emp = new Employee();//
                cust = response.Content.ReadAsAsync<Product>().Result;
                cust.ProductTypeList = db.ProductTypes.ToList();
                cust.ProductStatusList = db.ProductStatus.ToList();
                return View(cust);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(Product prod,int ProductTypeID,int ProductStatusID)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Product";
            v.ChangesMade = "Viewed Product";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Product";
            n.ChangesMade = "Created New Product";
            n.AuditLogTypeID = 2;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();

            Product model_ = new Product();
            
            model_ = prod;
            prod.ProductTypeID = ProductTypeID;
            prod.ProductStatusID = ProductStatusID;

            if (prod.ProductID == 0)
            {
                prod.QuantityOnHand = 0;
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Product", model_).Result;
                v.DateAccessed = DateTime.Now;
                v.TableAccessed = "Product";
                v.ChangesMade = "Viewed Product";
                v.AuditLogTypeID = 1;
                //v.UserID = eqp.user.UserID;
                db.AuditLogs.Add(v);

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Product";
                n.ChangesMade = "Created New Product";
                n.AuditLogTypeID = 2;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Product/" + model_.ProductID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Product";
                n.ChangesMade = "Updated Product";
                n.AuditLogTypeID = 3;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Product/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Product";
            v.ChangesMade = "Viewed Product";
            v.AuditLogTypeID = 1;
            //v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Product";
            n.ChangesMade = "Deleted Product";
            n.AuditLogTypeID = 4;
            //n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult WriteOffStock(int id=0)
        {
            if (id ==0)
            {
                return View(new Product());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Product>().Result);
            }
           
        }

        [HttpPost]
        public ActionResult WriteOffStock(Product writeOff)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Product/" + writeOff.ProductID, writeOff).Result;
            TempData["SuccessMessage"] = "Written-off Successfully";
            return RedirectToAction("Index");

        }
        private SpartanFireDBEntities1 context = new SpartanFireDBEntities1();
        public ActionResult ExportProducts()
        {
            
            List<Product> allCustomer = new List<Product>();
            allCustomer = context.Products.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ProductReport.rpt"));

            rd.SetDataSource(allCustomer);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ProductQuantityList.pdf");
        }
    }
}