using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snow_System.Models;

namespace Snow_System.Controllers
{

    public class HomeController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Home()
        {
            GlobalVariable model = new GlobalVariable();
            if(Globals.Username != "" && Globals.Password != "")
            {
                var userDetails = db.Users.Where(x => x.UserEmail == Globals.Username && x.UserPassword == Globals.Password).FirstOrDefault();
                model.UserRoleId = userDetails.UserRoleID;
            }
            else
            {
                model = null;
            }
            return View(model);
        }
        public ActionResult RateService()
        {
            return View();
        }
        public ActionResult LodgeComplaint()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchService()
        {
            return View();
        }

        public ActionResult ServiceInformation()
        {
            return View();
        }

        public ActionResult WriteoffStock()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }



        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult ForgotPasswordEmail()
        {
            return View();
        }


        /*
        private List<Product> ProductList = new List<Product>();
        private List<Employee> AssignedEmployees = new List<Employee>();
        private List<Supplier> Suppliers = new List<Supplier>();
        private List<ProductType> ProductTypes = new List<ProductType>();
        private List<SupplierOrder> supplierOrders = new List<SupplierOrder>();
        private List<Location> Locations = new List<Location>();
        private List<ProductOrder> ProductOrders = new List<ProductOrder>();
        private List<Vehicle> Vehicles = new List<Vehicle>();
        private List<EmployeeType> EmployeeTypes = new List<EmployeeType>();
        private List<Delivery> Deliveries = new List<Delivery>();
        private List<Client> Clients = new List<Client>();
        private List<Equipment> AllEquipment = new List<Equipment>();


        
        public ActionResult About()
        {
            return View();
        }

        public ActionResult MakeSupplierOrder()
        {
            //test data
            Product p = new Product();
            Product p2 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 5L CO2";
            p2.ProductDescription = "Red 5L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500;
            ProductList.Add(p2);

            return View(ProductList);
        }

        public ActionResult AssignDelivery()
        {
            Employee e = new Employee();
            e.Name = "Kananelo";
            e.Surname = "Thobakgale";
            e.Certification = "SQSA";
            e.EmployeeType = "Technician";
            AssignedEmployees.Add(e);

            return View(AssignedEmployees);
        }

        public ActionResult CapturePayment()
        {
            return View();
        }

        public ActionResult PackOrder()
        {
            //test data
            Product p = new Product();
            Product p2 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 5L CO2";
            p2.ProductDescription = "Red 5L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500;
            ProductList.Add(p2);

            return View(ProductList);
        }

        public ActionResult returnSupplierOrder()
        {
            //test data
            Product p = new Product();
            Product p2 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 5L CO2";
            p2.ProductDescription = "Red 5L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500;
            ProductList.Add(p2);

            return View(ProductList);
        }

        public ActionResult ReceiveSupplierOrder()
        {
            //test data
            Product p = new Product();
            Product p2 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 5L CO2";
            p2.ProductDescription = "Red 5L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500;
            ProductList.Add(p2);

            return View(ProductList);
        }
        public ActionResult CancelSupplierOrder()
        {
            //test data
            Product p = new Product();
            Product p2 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 5L CO2";
            p2.ProductDescription = "Red 5L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500;
            ProductList.Add(p2);

            return View(ProductList);
        }
        public ActionResult AddSupplier()
        {
            return View();
        }

        public ActionResult SearchSupplier()
        {
            Supplier s = new Supplier();
            s.supplierName = "Fire Suppliers";
            s.registrationNumber = "15168547631";
            s.emailAddress = "contact@firesuppliers.co.za";
            s.creditBalance = 1500.00;
            s.contactNumber = "0118735778";
            Suppliers.Add(s);
            Supplier n = new Supplier();
            n.supplierName = "Water Suppliers";
            n.registrationNumber = "651654163516";
            n.emailAddress = "contact@watersuppliers.co.za";
            n.creditBalance = 1500.00;
            n.contactNumber = "0118735778";
            Suppliers.Add(n);

            return View(Suppliers);
        }

        public ActionResult AddProductType()
        {
            return View();
        }

        public ActionResult SearchProductType()
        {
            ProductType s = new ProductType();
            s.productTypeDescription = "Extinguisher";
            ProductTypes.Add(s);
            ProductType n = new ProductType();
            n.productTypeDescription = "Sign";
            ProductTypes.Add(n);

            return View(ProductTypes);
        }

        public ActionResult EditSupplier()
        {
            
            Supplier s = new Supplier();
            s.supplierName = "Fire Suppliers";
            s.registrationNumber = "15168547631";
            s.emailAddress = "contact@firesuppliers.co.za";
            s.creditBalance = 1500.00;
            s.contactNumber = "0118735778";

            return View(s);
            
        }

        public ActionResult EditProductType()
        {
            ProductType s = new ProductType();
            s.productTypeDescription = "Extinguisher";
            return View(s);
        }

        public ActionResult DeleteSupplier()
        {
            Supplier s = new Supplier();
            s.supplierName = "Fire Suppliers";
            s.registrationNumber = "15168547631";
            s.emailAddress = "contact@firesuppliers.co.za";
            s.creditBalance = 1500.00;
            s.contactNumber = "0118735778";

            return View(s);
        }

        public ActionResult DeleteProductType()
        {
            ProductType s = new ProductType();
            s.productTypeDescription = "Extinguisher";

            return View(s);
        }

        public ActionResult ChooseEmployee()
        {
            Employee e = new Employee();
            e.Name = "Kananelo";
            e.Surname = "Thobakgale";
            e.Certification = "SQSA";
            e.EmployeeType = "Technician";
            AssignedEmployees.Add(e);
            Employee s = new Employee();
            s.Name = "Bob";
            s.Surname = "The Builder";
            s.Certification = "Who knows";
            s.EmployeeType = "Builder";
            AssignedEmployees.Add(s);
            Employee r = new Employee();
            r.Name = "Steve";
            r.Surname = "Steve";
            r.Certification = "Steve";
            r.EmployeeType = "Steve";
            AssignedEmployees.Add(r);

            return View(AssignedEmployees);
        }

        public ActionResult ChooseProduct()
        {
            Product p = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850.00;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 10L CO2";
            p2.ProductDescription = "Red 10L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500.00;
            ProductList.Add(p2);
            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);

            return View(ProductList);
        }
        public ActionResult SearchSupplierOrder()
        {
            return View(supplierOrders);
        }

        public ActionResult ConfirmPaymentFromClient()
        {
            ProductOrder po = new ProductOrder();
            po.ClientName = "Benjamin Abrahams";
            po.DateOrdered = DateTime.Now;
            po.orderID = 156454654;

            return View(po);
        }

        public ActionResult ConfirmCreation()
        {
            return View();
        }

        public ActionResult ConfirmEdit()
        {
            return View();
        }

        public ActionResult SuccessfulCreate()
        {
            return View();
        }

        public ActionResult SuccessfulEdit()
        {
            return View();
        }

        public ActionResult SuccessfulDelete()
        {
            return View();
        }

        public ActionResult UnsuccessfulAdd()
        {
            return View();
        }

        public ActionResult UnsuccessfulEdit()
        {
            return View();
        }

        public ActionResult UnsuccefulDelete()
        {
            return View();
        }
        public ActionResult SuccessfulSupplierOrderPlaced()
        {
            return View();
        }

        public ActionResult UnsuccessfulSupplierOrderPlaced()
        {
            return View();
        }

        public ActionResult AddVehicle()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            Product p = new Product();
            Product p2 = new Product();
            Product p3 = new Product();

            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850.00;
            ProductList.Add(p);

            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 10L CO2";
            p2.ProductDescription = "Red 10L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500.00;
            ProductList.Add(p2);

            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);

           

            return View(ProductList);
        }

        public ActionResult Products()
        {
            Product p = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            Product p4 = new Product();
            Product p5 = new Product();
            Product p6 = new Product();
            Product p7 = new Product();
            Product p8 = new Product();
            Product p9 = new Product();

            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850.00;
            ProductList.Add(p);

            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 10L CO2";
            p2.ProductDescription = "Red 10L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500.00;
            ProductList.Add(p2);

            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);

            p4.ProductID = 0002;
            p4.ProductName = " Exit Sign";
            p4.ProductDescription = "Sign denoting the exit of a building";
            p4.ProductType = "Signage";
            p4.ProductQuantityOnHand = 6;
            p4.ProductSellingPrice = 25.00;
            ProductList.Add(p4);

            p5.ProductID = 0002;
            p5.ProductName = " Exit Sign";
            p5.ProductDescription = "Sign denoting the exit of a building";
            p5.ProductType = "Signage";
            p5.ProductQuantityOnHand = 6;
            p5.ProductSellingPrice = 25.00;
            ProductList.Add(p5);

            p6.ProductID = 0002;
            p6.ProductName = " Exit Sign";
            p6.ProductDescription = "Sign denoting the exit of a building";
            p6.ProductType = "Signage";
            p6.ProductQuantityOnHand = 6;
            p6.ProductSellingPrice = 25.00;
            ProductList.Add(p6);

            p7.ProductID = 0002;
            p7.ProductName = " Exit Sign";
            p7.ProductDescription = "Sign denoting the exit of a building";
            p7.ProductType = "Signage";
            p7.ProductQuantityOnHand = 6;
            p7.ProductSellingPrice = 25.00;
            ProductList.Add(p7);

            p8.ProductID = 0002;
            p8.ProductName = " Exit Sign";
            p8.ProductDescription = "Sign denoting the exit of a building";
            p8.ProductType = "Signage";
            p8.ProductQuantityOnHand = 6;
            p8.ProductSellingPrice = 25.00;
            ProductList.Add(p8);

            return View(ProductList);
        }

        public ActionResult ChooseLocationDelivery()
        {
            Location l1 = new Location();
            l1.ID = 0001;
            l1.LocationName = "Head Office";
            l1.StreetAddress = "7 Wallabey Way";
            l1.Suburb = "Centurion";
            l1.City = "Pretoria";
            l1.PostalCode = 2144;
            l1.ContactPersonName = "Steven Wasniack";
            l1.ContactPersonNumber = "085 644 2115";

            Locations.Add(l1);

            Location l2 = new Location();
            l2.ID = 0002;
            l2.LocationName = "Woolworths north pretoria";
            l2.StreetAddress = "2 Lollipop Road";
            l2.Suburb = "Pretoria North";
            l2.City = "Pretoria";
            l2.PostalCode = 2564;
            l2.ContactPersonName = "Manager";
            l2.ContactPersonNumber = "011 357 3685";

            Locations.Add(l2);
            return View(Locations);
        }

        public ActionResult ChooseLocationService()
        {
            Location l1 = new Location();
            l1.ID = 0001;
            l1.LocationName = "Head Office";
            l1.StreetAddress = "7 Wallabey Way";
            l1.Suburb = "Centurion";
            l1.City = "Pretoria";
            l1.PostalCode = 2144;
            l1.ContactPersonName = "Steven Wasniack";
            l1.ContactPersonNumber = "085 644 2115";

            Locations.Add(l1);

            Location l2 = new Location();
            l2.ID = 0002;
            l2.LocationName = "Woolworths north pretoria";
            l2.StreetAddress = "2 Lollipop Road";
            l2.Suburb = "Pretoria North";
            l2.City = "Pretoria";
            l2.PostalCode = 2564;
            l2.ContactPersonName = "Manager";
            l2.ContactPersonNumber = "011 357 3685";

            Locations.Add(l2);
            return View(Locations);
        }

        public ActionResult OrderInformation()
        {
            Product p = new Product();
            Product p2 = new Product();
            Product p3 = new Product();

            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850.00;
            ProductList.Add(p);

            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 10L CO2";
            p2.ProductDescription = "Red 10L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500.00;
            ProductList.Add(p2);

            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);



            return View(ProductList);
        }


        public ActionResult SearchProductOrders()
        {
            ProductOrders.Add(new ProductOrder());
            return View(ProductOrders);
        }

        public ActionResult SearchProduct()
        {
            Product p = new Product();
            Product p2 = new Product();
            Product p3 = new Product();

            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850.00;
            ProductList.Add(p);

            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 10L CO2";
            p2.ProductDescription = "Red 10L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500.00;
            ProductList.Add(p2);

            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);


            return View(ProductList);
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult SuccessfulAssignation()
        {
            return View();
        }

        public ActionResult UnsuccessfulAssignation()
        {
            return View();
        }

        public ActionResult ConfirmCancellation()
        {
            return View();
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }

        public ActionResult ConfirmReturn()
        {
            return View();
        }

        public ActionResult ContinueToAssign()
        {
            return View();
        }

        public ActionResult ContinueToBookService()
        {
            return View();
        }

        public ActionResult DeleteVehicle()
        {
            Vehicle v = new Vehicle();
            v.LicensePlateNumber = "TYY 336 GP";
            v.PurchaseDate = DateTime.Now;
            v.VehicleDescription = "First Wrokshop";
            v.VeihcleType = "Workshop";
            return View(v);
        }

        public ActionResult DeleteProduct()
        {
            Product p3 = new Product();
            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);
            return View(p3);
        }

        public ActionResult EditProduct()
        {
            Product p3 = new Product();
            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);
            return View(p3);
        }

        public ActionResult EditVehicle()
        {
            Vehicle v = new Vehicle();
            v.LicensePlateNumber = "TYY 336 GP";
            v.PurchaseDate = DateTime.Now;
            v.VehicleDescription = "First Wrokshop";
            v.VeihcleType = "Workshop";
            return View(v);
        }
        public ActionResult MakePayment()
        {
            return View();
        }

        public ActionResult ServiceBooked()
        {
            return View();
        }

        public ActionResult SuccessfulCaptureClientPayment()
        {
            return View();
        }

        public ActionResult SuccessfullCancellation()
        {
            return View();
        }

        public ActionResult SuccessfulReturnRequest()
        {
            return View();
        }

        public ActionResult UnsuccessfulCancellation()
        {
            return View();
        }

        public ActionResult UnsuccessfulCaptureClientPayment()
        {
            return View();
        }

        public ActionResult UnsuccessfulReturnRequest()
        {
            return View();
        }

        public ActionResult WriteoffStock()
        {
            return View();
        }

        public ActionResult ConfirmWriteOff()
        {
            return View();
        }

        public ActionResult SuccessfulWriteOff()
        {
            return View();
        }

        public ActionResult UnsuccessfulWriteOff()
        {
            return View();
        }

        public ActionResult ConfirmSupplierOrderRecieved()
        {
            return View();
        }

        public ActionResult SuccessfulOrderPlaced()
        {
            return View();
        }

        public ActionResult UnsuccessfulOrderPlaced()
        {
            return View();
        }

        public ActionResult ConfirmSupplierPayment()
        {
            return View();
        }

        public ActionResult SuccessfulSupplierPayment()
        {
            return View();
        }

        public ActionResult UnsuccessfulSupplierPayment()
        {
            return View();
        }

        public ActionResult SearchVehicle()
        {
            Vehicle v = new Vehicle();
            v.LicensePlateNumber = "TYY 336 GP";
            v.PurchaseDate = DateTime.Now;
            v.VehicleDescription = "First Wrokshop";
            v.VeihcleType = "Workshop";
            Vehicles.Add(v);
            return View(Vehicles);
        }

        public ActionResult AddVAT()
        {
            return View();
        }

        public ActionResult VAT()
        {
            List<VAT> VATList = new List<VAT>();
            VAT v = new VAT();
            v.Percent = 15;
            v.StartDate = DateTime.Now;

            VATList.Add(v);
            return View(VATList);
        }

        public ActionResult AddLocation()
        {
            return View();
        }

        public ActionResult AddEmployeeType()
        {
            return View();
        }

        public ActionResult EditEmployeeType()
        {
            EmployeeType et = new EmployeeType();
            et.EmployeeTypeName = "Technician";
            return View(et);
        }

        public ActionResult SearchEmployeeType()
        {
            EmployeeType et = new EmployeeType();
            et.EmployeeTypeName = "Technician";
            EmployeeTypes.Add(et);
            return View(EmployeeTypes);
        }

        public ActionResult SearchDelivery()
        {
            Delivery d = new Delivery();
            d.DeliveryDate = DateTime.Now;
            Deliveries.Add(d);
            return View(Deliveries);
        }


        public ActionResult AddClient()
        {
            return View();
        }

        public ActionResult SearchClient()
        {
            Client c = new Client();
            c.ClientEmail = "email@email.co.za";
            c.ClientType = "Private";
            c.Name = "Dovi";
            c.Surname = "Livingstone";
            c.PhysicalAddress = "90 Deathwinter Rd";
            Clients.Add(c);
            return View(Clients);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult EditClient()
        {
            Client c = new Client();
            c.ClientEmail = "email@email.co.za";
            c.ClientType = "Private";
            c.Name = "Dovi";
            c.Surname = "Livingstone";
            c.PhysicalAddress = "90 Deathwinter Rd";
            Clients.Add(c);
            return View(Clients[0]);
        }

        public ActionResult ForgotPasswordEmail()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult TakeInReturnedStock()
        {
            Product p = new Product();
            Product p2 = new Product();
            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850;
            ProductList.Add(p);
            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 5L CO2";
            p2.ProductDescription = "Red 5L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500;
            ProductList.Add(p2);

            return View(ProductList);
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        public ActionResult ForgotPasswordEmailSent()
        {
            return View();
        }

        public ActionResult EditEmployee()
        {
            return View();
        }

        public ActionResult SearchEmployee()
        {
            Employee e = new Employee();
            e.Name = "Kananelo";
            e.Surname = "Thobakgale";
            e.Certification = "SQSA";
            e.EmployeeType = "Technician";
            AssignedEmployees.Add(e);

            return View(AssignedEmployees);
        }

        public ActionResult RateService()
        {
            return View();
        }

        public ActionResult AddEquipment()
        {
            return View();
        }

        public ActionResult EditEquipment()
        {
            Equipment e = new Equipment();
            e.EquipmentDescription = "Preassure checker";
            e.Quantity = 5;
            return View(e);
        }

        public ActionResult SearchEquipment()
        {
            Equipment e = new Equipment();
            e.EquipmentDescription = "Preassure checker";
            e.Quantity = 5;
            AllEquipment.Add(e);
            return View(AllEquipment);
        }

        public ActionResult ReschedualeBooking()
        {
            ServiceRequest sr = new ServiceRequest();
            return View(sr);
        }

        public ActionResult LodgeComplaint()
        {
            ServiceRequest sr = new ServiceRequest();
            return View(sr);
        }

        public ActionResult AssignService()
        {
            return View();
        }

        public ActionResult ServiceInformation()
        {
            Product p = new Product();
            Product p2 = new Product();
            Product p3 = new Product();

            p.ProductID = 0001;
            p.ProductName = " Extinguisher 2L CO2";
            p.ProductDescription = "Red 2L Corbon dioxide Extinguisher";
            p.ProductType = "Extinguisher";
            p.ProductQuantityOnHand = 5;
            p.ProductSellingPrice = 850.00;
            ProductList.Add(p);

            p2.ProductID = 0002;
            p2.ProductName = " Extinguisher 10L CO2";
            p2.ProductDescription = "Red 10L Corbon dioxide Extinguisher";
            p2.ProductType = "Extinguisher";
            p2.ProductQuantityOnHand = 3;
            p2.ProductSellingPrice = 1500.00;
            ProductList.Add(p2);

            p3.ProductID = 0002;
            p3.ProductName = " Exit Sign";
            p3.ProductDescription = "Sign denoting the exit of a building";
            p3.ProductType = "Signage";
            p3.ProductQuantityOnHand = 6;
            p3.ProductSellingPrice = 25.00;
            ProductList.Add(p3);

            return View(ProductList);
        }

        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult ImportData()
        {
            return View();
        }
       
        public ActionResult ExportData()
        {
            return View();
        }

        public ActionResult ViewAuditLog()
        {
            return View();
        }
    }
    */

    }
}