using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Snow_System.Controllers;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Snow_System.Controllers
{
    public class SupplierController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        private SpartanFireDBEntities1 context = new SpartanFireDBEntities1(); // for suppier report
        // GET: Supplier
        public ActionResult Index(string searchBy,string search)
        {
            if (searchBy == "ContactNumber")
            {
                return View(db.Suppliers.Where(x => x.ContactNumber.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "Registration")
            {
                return View(db.Suppliers.Where(x => x.RegistrationNumber.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Suppliers.Where(x => x.SupplierName.StartsWith(search) || search == null).ToList());

            }
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcSupplierModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Supplier/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcSupplierModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcSupplierModel sup)
        {
            if (sup.SupplierID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Supplier", sup).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Supplier/" + sup.SupplierID, sup).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Supplier/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }


        public ActionResult CapturePayment(int id = 0)
        {
            Supplier supp;
            HttpResponseMessage re = GlobalVariables.WebAPIClient.GetAsync("Supplier/" + id.ToString()).Result;
            supp = re.Content.ReadAsAsync<Supplier>().Result;
            return View(supp);
        }

        [HttpPost]
        public ActionResult CapturePayment(Supplier balance)
        {
            Supplier supp;
            HttpResponseMessage re = GlobalVariables.WebAPIClient.GetAsync("Supplier/" + balance.SupplierID.ToString()).Result;
            supp = re.Content.ReadAsAsync<Supplier>().Result;
            supp.CreditBalance = balance.CreditBalance;
            HttpResponseMessage update = GlobalVariables.WebAPIClient.PutAsJsonAsync("supplier/" + balance.SupplierID, supp).Result;
            TempData["SuccessMessage"] = "Updated Successfully";
            return RedirectToAction("Index","Supplier");
        }

        public ActionResult SearchSupplierOrder()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return View(db.SupplierOrders.Include(so => so.Supplier).Include(so => so.SupplierOrderLines.Select(sol => sol.Product)).Include(so=>so.SupplierStatu).ToList()); 
        }

        public ActionResult SupplierOrderInformation(int? id)
        {
            IEnumerable<mvcVATModel> vt;

            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("VAT").Result;
            vt = response.Content.ReadAsAsync<IEnumerable<mvcVATModel>>().Result;
            ViewBag.VAT = vt.Last().VATRate;
            db.Configuration.ProxyCreationEnabled = false;
            return View(db.SupplierOrders.Include(so => so.Supplier)
                                        .Include(so => so.SupplierOrderLines.Select(sol => sol.Product))
                                        .Include(so => so.SupplierStatu)
                                        .Where(so=>so.SupplierOrderID == id).FirstOrDefault());
        }

        public RedirectToRouteResult UpdatingStatus(int? id, int? sid)
        {
            SupplierOrder so;
            db.Configuration.ProxyCreationEnabled = false;
            so = db.SupplierOrders.Find(id);
            so.SupplierStatusID = (int)sid;
            if (sid == 5)
            {
                so.SupplierOrderLines = db.SupplierOrderLines.Include(sol => sol.Product).Where(sol => sol.SupplierOrderID == id).ToList();
                foreach (var item in so.SupplierOrderLines)
                {
                    item.Product.QuantityOnHand = -item.QuantityReceived;
                }
                db.SaveChanges();
            }
            HttpResponseMessage update = GlobalVariables.WebAPIClient.PutAsJsonAsync("SupplierOrder/" + id, so).Result;
            return RedirectToAction("SupplierOrderInformation", new { id = so.SupplierOrderID });
        }

        public ActionResult MakeSupplierOrder(int? id)
        {
            ViewBag.products = db.Products.ToList();
            db.Configuration.ProxyCreationEnabled = false;
            SupplierOrder so = db.SupplierOrders.Where(order => order.SupplierID == id)
                                                .Where(order => order.SupplierStatusID == 1)
                                                .Include(order => order.SupplierOrderLines)
                                                .Include(order=>order.Supplier)
                                                .FirstOrDefault();
            if (so == null){
                so = new SupplierOrder();
                so.SupplierID = Convert.ToInt32(id);
                so.SupplierStatusID = 1;
                db.SupplierOrders.Add(so);
                so.Supplier = db.Suppliers.Where(sup => sup.SupplierID == so.SupplierID).FirstOrDefault();
                so.SupplierID = so.Supplier.SupplierID;
                
                db.SaveChanges();
            }
            TempData["SuppOrdID"] = so.SupplierOrderID;

            return View(so);
        }
        
        public ActionResult AddToList(int Product, int Quantity)
        {
            db.Configuration.ProxyCreationEnabled = false;
            bool inList = false;
            int SuppOrdID = Convert.ToInt32(TempData["SuppOrdID"]);
            SupplierOrder so = db.SupplierOrders.Include(sos=>sos.Supplier)
                                                .Include(sos=>sos.SupplierOrderLines)
                                                .Where(sos=>sos.SupplierOrderID == SuppOrdID).FirstOrDefault();
            foreach(SupplierOrderLine sol in so.SupplierOrderLines)
            {
                if(sol.ProductID == Product)
                {
                    inList = true;
                    sol.QuantityOrdered += Quantity;
                    db.SaveChanges();
                    break;
                }
            }
            if (!inList)
            {
                SupplierOrderLine newLine = new SupplierOrderLine();
                newLine.ProductID = Product;
                newLine.QuantityOrdered = Quantity;
                newLine.QuantityReceived = 0;
                newLine.SupplierOrderID = Convert.ToInt32(TempData["SuppOrdID"]);
                so.SupplierOrderLines.Add(newLine);
                db.SaveChanges();
            }
            return RedirectToAction("MakeSupplierOrder" , new { id= so.SupplierID });
        }

        public ActionResult RemoveAll()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int SuppOrdID = Convert.ToInt32(TempData["SuppOrdID"]);
            SupplierOrder so = db.SupplierOrders
                .Include(sl=>sl.SupplierOrderLines)
                .Where(sl=>sl.SupplierOrderID == SuppOrdID)
                .FirstOrDefault();
            List<SupplierOrderLine> soll = so.SupplierOrderLines.ToList();
            foreach(SupplierOrderLine sol in soll)
            {
                so.SupplierOrderLines.Remove(sol);
            }
            db.SaveChanges();
            return RedirectToAction("MakeSupplierOrder", new { id = TempData["SuppOrdID"] });
        }

        public ActionResult Remove(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int SuppOrdID = Convert.ToInt32(TempData["SuppOrdID"]);
            SupplierOrder so = db.SupplierOrders
                .Include(sl => sl.SupplierOrderLines)
                .Where(sl => sl.SupplierOrderID == SuppOrdID)
                .FirstOrDefault();
            foreach (SupplierOrderLine sol in so.SupplierOrderLines)
            {
                if(sol.ProductID == id)
                {
                    so.SupplierOrderLines.Remove(sol);
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("MakeSupplierOrder", new { id = TempData["SuppOrdID"] });
        }


        public ActionResult FinalizeOrder(string options)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int SuppOrdID = Convert.ToInt32(TempData["SuppOrdID"]);
            SupplierOrder so = db.SupplierOrders
                .Where(sl => sl.SupplierOrderID == SuppOrdID)
                .Include(sl=>sl.Supplier)
                .Include(sl=>sl.SupplierOrderLines.Select(l=>l.Product))
                .FirstOrDefault();
            so.SupplierStatusID = 6;
            so.OrderDate = DateTime.Today;
            db.SaveChanges();
            if (options == "yes")
            {
                try
                {
                    SendEmailController sendemail = new SendEmailController();
                    sendemail.NewSupplierOrderEmail(so);
                    TempData["SuccessMessage"] = "Order request made - Email Sent";
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    TempData["ErrorMessage"] = "Email error - Email not sent";
                    return RedirectToAction("Index");
                }
            }
            TempData["SuccessMessage"] = "Order request made";
            return RedirectToAction("Index");
        }


        public ActionResult ExportSupplier()
        {
            List<Supplier> allCustomer = new List<Supplier>();
            allCustomer = context.Suppliers.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "SupplierReport.rpt"));

            rd.SetDataSource(allCustomer);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SupplierList.pdf");
        }

    }
}