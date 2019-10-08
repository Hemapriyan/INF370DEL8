using OfficeOpenXml;
using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ExportController : Controller
    {
        // GET: Export
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();


        public ActionResult ExportData()
        {

            return View();

        }

        public FilePathResult ExportEmployeeDataToExcel()
        {


            IEnumerable<mvcEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Employees").Result;

            empList = (response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result).ToList();


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("EmployeeData");

            //ws.Cells["A1"].Value = "Communication";
            //ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Employee Data";
            //ws.Cells["B2"].Value = "Employee Data1";

            ws.Cells["A1"].Value = "Date";
            ws.Cells["B1"].Value = string.Format("{0:dd MMMM yyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Name";
            ws.Cells["B6"].Value = "Surname";
            ws.Cells["C6"].Value = "IDNumber";
            ws.Cells["D6"].Value = "HouseAddress";

            int rowStart = 7;
            foreach (var item in empList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Surname;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.IDNumber;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.HouseAddress;
                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-disposition", "attachement: filename=" + "EmployeeData.xlsx");

            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
            return File("application/vnd.ms-excel", "EmployeeData.xlsx");

        }
        // Export Client 
        public ActionResult ExportClientData()
        {
            IEnumerable<mvcClientModel> ClientList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Client").Result;

            ClientList = response.Content.ReadAsAsync<IEnumerable<mvcClientModel>>().Result;

            return View(ClientList);


        }
        public FilePathResult ExportClientDataToExcel()
        {
            IEnumerable<mvcClientModel> ClientList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Client").Result;

            ClientList = (response.Content.ReadAsAsync<IEnumerable<mvcClientModel>>().Result).ToList();

            ExcelPackage pck1 = new ExcelPackage();
            ExcelWorksheet ws = pck1.Workbook.Worksheets.Add("ClientData");

            ws.Cells["A2"].Value = "Client Data";

            ws.Cells["A1"].Value = "Date";
            ws.Cells["B1"].Value = string.Format("{0:dd MMMM yyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Name";
            ws.Cells["B6"].Value = "Surname";
            ws.Cells["C6"].Value = "Contact Number";
            ws.Cells["D6"].Value = "Email Address";
            ws.Cells["E6"].Value = "House Address";

            int rowStart = 7;
            foreach (var item in ClientList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ClientName;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.ClientSurname;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.ContactNumber;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.EmailAddress;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.HouseAddress;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-disposition", "attachement: filename=" + "ClientData.xlsx");

            Response.BinaryWrite(pck1.GetAsByteArray());
            Response.End();
            return File("application/vnd.ms-excel", "ClientData.xlsx");





        }

        public FilePathResult ExportProductDataToExcel()
        {

            IEnumerable<mvcProductModel> ProductList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Product").Result;

            ProductList = (response.Content.ReadAsAsync<IEnumerable<mvcProductModel>>().Result).ToList();

            ExcelPackage pck2 = new ExcelPackage();
            ExcelWorksheet ws = pck2.Workbook.Worksheets.Add("ProductData");

            ws.Cells["A2"].Value = "Product Data";

            ws.Cells["A1"].Value = "Date";
            ws.Cells["B1"].Value = string.Format("{0:dd MMMM yyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Name";
            ws.Cells["B6"].Value = "Description";
            ws.Cells["C6"].Value = "Quantitiy On Hand";
            ws.Cells["D6"].Value = "Selling Price";

            int rowStart = 7;
            foreach (var item in ProductList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Dscription;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.QuantityOnHand;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.SellingPrice;

                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-disposition", "attachement: filename=" + "ProductData.xlsx");

            Response.BinaryWrite(pck2.GetAsByteArray());
            Response.End();
            return File("application/vnd.ms-excel", "ProductData.xlsx");

        }
        public FilePathResult ExportSupplierDataToExcel()
        {
            IEnumerable<mvcSupplierModel> SupplierList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Supplier").Result;

            SupplierList = (response.Content.ReadAsAsync<IEnumerable<mvcSupplierModel>>().Result).ToList();

            ExcelPackage pck3 = new ExcelPackage();
            ExcelWorksheet ws = pck3.Workbook.Worksheets.Add("SupplierData");


            ws.Cells["A2"].Value = "Supplier Data";

            ws.Cells["A1"].Value = "Date";
            ws.Cells["B1"].Value = string.Format("{0:dd MMMM yyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Supplier Name";
            ws.Cells["B6"].Value = "Contact Number";
            ws.Cells["C6"].Value = "Email Address";
            ws.Cells["D6"].Value = "Account Balance";

            int rowStart = 7;
            foreach (var item in SupplierList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ContactPersonName;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.ContactNumber;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.EmailAddress;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.CreditBalance;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-disposition", "attachement: filename=" + "SupplierData.xlsx");

            Response.BinaryWrite(pck3.GetAsByteArray());
            Response.End();
            return File("application/vnd.ms-excel", "SupplierData.xlsx");

        }
    }
}