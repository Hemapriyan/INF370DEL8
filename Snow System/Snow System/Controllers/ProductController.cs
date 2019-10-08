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
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Description")
            {
                return View(db.Products.Where(x => x.ProductDescription.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Products.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
        }


        public ActionResult AddorEdit(int id = 0)
        {
            
            if (id == 0)
                return View(new mvcProductModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcProductModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcProductModel prod)
        {
           
            if (prod.ProductID == 0)
            {

                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Product", prod).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Product/" + prod.ProductID, prod).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Product/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }

        public ActionResult WriteOffStock(int id=0)
        {
            if (id ==0)
            {
                return View(new mvcProductModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcProductModel>().Result);
            }
           
        }

        [HttpPost]
        public ActionResult WriteOffStock(mvcProductModel writeOff)
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