using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ClientReportController : Controller
    {
        // GET: ClientReport
        //DbContext  
        private SpartanFireDBEntities1 context = new SpartanFireDBEntities1();
        
        // GET: Customer  
        public ActionResult Index()
        {
            var customerList = context.Clients.ToList();
            return View(customerList);
        }


        public ActionResult ExportCustomers()
        {
            List<Client> allCustomer = new List<Client>();
            allCustomer = context.Clients.ToList();

            

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ClientReport.rpt"));
            
            rd.SetDataSource(allCustomer);
            
            

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");
        }

    }
}