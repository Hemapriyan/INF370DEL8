using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ProductOrderController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET: ProductOrder
        public ActionResult Index()
        {
            int userRole = Convert.ToInt32(Session["UserRoleID"]);
            ViewBag.roleID = userRole;
            IEnumerable <ProductOrder> orders;
            if (userRole >= 2)
            {
                orders = db.ProductOrders.Include(po=>po.Location)
                                        .Include(po=>po.Location.Client)
                                        .Include(po=>po.ProductOrderLines)
                                        .Include(po=>po.ProductOrderStatu)
                                        .ToList();
            }
            else
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                orders = db.ProductOrders.Where(po => po.Location.ClientID == userID)
                                        .Include(po => po.Location)
                                        .Include(po => po.Location.Client)
                                        .Include(po => po.ProductOrderLines)
                                        .Include(po => po.ProductOrderStatu)
                                        .ToList();
            }
            return View(orders);
        }

        public ActionResult OrderInformation(int? id)
        {
            ProductOrder order = new ProductOrder();
            int newID = Convert.ToInt32(id);
            order = db.ProductOrders.Where(po => po.ProductOrderID == newID)
                                        .Include(po => po.Location)
                                        .Include(po => po.Location.Client)
                                        .Include(po => po.ProductOrderLines)
                                        .Include(po => po.ProductOrderStatu)
                                        .FirstOrDefault();
            IEnumerable<mvcVATModel> vt;

            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("VAT").Result;
            vt = response.Content.ReadAsAsync<IEnumerable<mvcVATModel>>().Result;
            ViewBag.VAT = vt.Last().VATRate;
            return View(order);
        }

        public RedirectToRouteResult UpdatingStatus(int? id, int? sid)
        {
            ProductOrder po;
            db.Configuration.ProxyCreationEnabled = false;
            po = db.ProductOrders.Find(id);
            po.ProductOrderStatusID = (int)sid;
            HttpResponseMessage update = GlobalVariables.WebAPIClient.PutAsJsonAsync("ProductOrder/" + id, po).Result;
            return RedirectToAction("OrderInformation", new { id = po.ProductOrderID});
        }

        public ActionResult MakePayment()
        {
            db.Configuration.ProxyCreationEnabled = false;
            BankingDetail bd = db.BankingDetails.FirstOrDefault();
            return View(bd);
        }

        public ActionResult UpdatingStatusALt(int? id, int? sid)
        {
            ProductOrder po;
            db.Configuration.ProxyCreationEnabled = false;
            po = db.ProductOrders.Find(id);
            po.DateOfOrder = DateTime.Now;
            po.ProductOrderStatusID = (int)sid;
            HttpResponseMessage update = GlobalVariables.WebAPIClient.PutAsJsonAsync("ProductOrder/" + id, po).Result;
            return RedirectToAction("MakePayment", "ProductOrder");
        }

        public ActionResult TakeInReturnedStock(int? id)
        {
            ProductOrder po;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ProductOrder/" + id).Result;
            po = response.Content.ReadAsAsync<ProductOrder>().Result;

            return View(po);
        }

        public ActionResult Products()
        {
            IEnumerable<Product> prod;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product").Result;
            prod = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            return View(prod);
        }

        public ActionResult Checkout(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductOrder p = db.ProductOrders.Where(po => po.ProductOrderID == id)
                                             .Include(pr => pr.Location)
                                             .Include(pr=> pr.Location.Client)
                                             .Include(po => po.ProductOrderLines.Select(line => line.Product))
                                             .Include(po => po.ProductOrderStatu)
                                             .FirstOrDefault();
            IEnumerable<VAT> vt = db.VATs.ToList();
            ViewBag.VAT = vt.Last().VATRate;
            return View(p);
        }

        public ActionResult AddProductToCart(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int user = Convert.ToInt32(Session["UserID"]);
            db.Configuration.ProxyCreationEnabled = false;
            mvcProductModel pm;
            HttpResponseMessage re = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;// finds the product
            pm = re.Content.ReadAsAsync<mvcProductModel>().Result;
            ProductOrder productorder = db.ProductOrders.Where(po => po.ProductOrderStatusID == 1)
                                                        .Include(po => po.Location)
                                                      .Include(po=>po.Location.Client)
                                                      .Include(po=>po.ProductOrderLines.Select(line=>line.Product))
                                                      .Where(po=>po.Location.Client.UserID == user)
                                                      .FirstOrDefault();
            if (productorder == null)//if no order started
            {
                ProductOrderLine pl = new ProductOrderLine();
                productorder = new ProductOrder();
                pl.ProductID = pm.ProductID;
                pl.QuantityOrdered = 1;
                pl.QuantityDelivered = 0;
                productorder.ProductOrderStatusID = 1;
                productorder.LocationID = db.Locations.Where(l => l.ClientID == user).Select(l=>l.LocationID).FirstOrDefault();
                db.ProductOrders.Add(productorder);
                productorder.Client_ID = db.Clients.Where(c=>c.UserID == user)
                    .Select(c=>c.ClientID).FirstOrDefault();
                db.SaveChanges();
                List<int> orderIDs = db.ProductOrders.Select(po => po.ProductOrderID).ToList();
                pl.ProductOrderID = orderIDs.Last();
                db.ProductOrderLines.Add(pl);
                db.SaveChanges();
            }
            else//if order started (in cart)
            {
                bool inCart = false;
                foreach(ProductOrderLine line in productorder.ProductOrderLines)
                {
                    if(line.ProductID == pm.ProductID)
                    {
                        inCart = true;
                        line.QuantityOrdered += 1;
                        db.SaveChanges();
                        break;
                    }
                }
                if (!inCart)
                {
                    ProductOrderLine tmp = new ProductOrderLine();
                    tmp.ProductID = pm.ProductID;
                    tmp.QuantityOrdered = 1;
                    tmp.QuantityDelivered = 0;
                    tmp.ProductOrderID = productorder.ProductOrderID;
                    productorder.ProductOrderLines.Add(tmp);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Checkout", "ProductOrder", new { id = productorder.ProductOrderID });
        }
        public ActionResult RemoveFromCart(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int user = Convert.ToInt32(Session["UserID"]);
            Product pm;
            HttpResponseMessage re = GlobalVariables.WebAPIClient.GetAsync("Product/" + id.ToString()).Result;// finds the product
            pm = re.Content.ReadAsAsync<Product>().Result;

            ProductOrder productorder = db.ProductOrders.Where(po => po.ProductOrderStatusID == 1)
                                          .Include(po => po.Location.Client)
                                          .Include(po => po.ProductOrderLines.Select(line => line.Product))
                                          .Where(po => po.Location.Client.UserID == user).FirstOrDefault();

            ProductOrderLine ln = productorder.ProductOrderLines.Where(pl => pl.ProductID == id).FirstOrDefault();

            productorder.ProductOrderLines.Remove(ln);
            db.SaveChanges();

            return RedirectToAction("Checkout", "ProductOrder", new { id = productorder.ProductOrderID });

        }

        public ActionResult MakeClientOrder(int? id)
        {
            TempData["ClientID"] = id;
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.products = db.Products.ToList();
            Client cnt = db.Clients.Where(c => c.ClientID == id).FirstOrDefault();
            ViewBag.ClientName = cnt.ClientName + " " + cnt.ClientSurname;
            ViewBag.ClientNumber = cnt.ContactNumber;
            //ViewBag.ClientEmail = cnt.EmailAddress;
            ProductOrder po = db.ProductOrders.Include(order => order.ProductOrderLines)
                                                .Include(order => order.ProductOrderStatu)
                                                .Include(order=>order.ProductOrderLines.Select(pol=>pol.Product.ProductType))
                                                .Where(order => order.Client_ID == id)
                                                .Where(order => order.ProductOrderStatusID == 1).FirstOrDefault();
            if (po != null)
            {

            }
            else
            {
                po = new ProductOrder();
                po.Client_ID = Convert.ToInt32(id);

                po.ProductOrderStatusID = 1;
                db.ProductOrders.Add(po);
                db.SaveChanges();
            }
            TempData["ProdOrdID"] = po.ProductOrderID;
            return View(po);
        }

        public ActionResult AddProduct(int Product, int Quantity)
        {
            bool inList = false;
            db.Configuration.ProxyCreationEnabled = false;
            int temp = Convert.ToInt32( TempData["ProdOrdID"]);
            ProductOrder po = db.ProductOrders.Include(order => order.ProductOrderLines)
                                    .Include(order => order.ProductOrderStatu)
                                    .Where(order => order.ProductOrderID == temp)
                                    .FirstOrDefault();
            foreach (ProductOrderLine pol in po.ProductOrderLines)
            {
                if(pol.ProductID == Product)
                {
                    inList = true;
                    pol.QuantityOrdered += Quantity;
                    break;
                }
            }
            if (!inList) 
            {
                ProductOrderLine pol = new ProductOrderLine();
                pol.ProductID = Product;
                pol.QuantityOrdered = Quantity;
                pol.QuantityDelivered = 0;
                po.ProductOrderLines.Add(pol);
            }
            db.SaveChanges();
            return RedirectToAction("MakeClientOrder", "ProductOrder", new { id = TempData["ClientID"] });
        }

        public ActionResult RemoveFromList(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int temp = Convert.ToInt32(TempData["ProdOrdID"]);
            int prod = Convert.ToInt32(id);
            ProductOrder po = db.ProductOrders.Include(order => order.ProductOrderLines)
                        .Include(order => order.ProductOrderStatu)
                        .Where(order => order.ProductOrderID == temp)
                        .FirstOrDefault();
            ProductOrderLine pol = po.ProductOrderLines.Where(p=>p.ProductID== prod).FirstOrDefault();
            po.ProductOrderLines.Remove(pol);
            db.SaveChanges();
            return RedirectToAction("MakeClientOrder", "ProductOrder", new { id = TempData["ClientID"] });

        }

        public ActionResult RemoveAllFromList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int temp = Convert.ToInt32(TempData["ProdOrdID"]);
            ProductOrder po = db.ProductOrders
                .Include(order => order.ProductOrderLines)
            .Include(order => order.ProductOrderStatu)
            .Where(order => order.ProductOrderID == temp)
            .FirstOrDefault();
            foreach (ProductOrderLine line in po.ProductOrderLines)
            {
                po.ProductOrderLines.Remove(line);
            }
            db.SaveChanges();
            return RedirectToAction("MakeClientOrder", "ProductOrder", new { id = TempData["ClientID"] });

        }

        public ActionResult ChooseLocation(int id)
        {
            int ordID = Convert.ToInt32(TempData["OrderID"]) ;
            ProductOrder por = db.ProductOrders.Where(po => po.ProductOrderID == ordID).FirstOrDefault();
            por.LocationID = id;
            por.DateOfOrder = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("MakePayment");
        }

    }
}