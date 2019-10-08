using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ProductReportController : Controller
    {
        // GET: ProductReport
        //DbContext  
        private SpartanFireDBEntities1 context = new SpartanFireDBEntities1();
        
        public ActionResult Index()
        {
            var productList = context.Products.ToList();
            return View(productList);
        }


        public ActionResult ExportProducts()
        {
            List<Product> allProduct = new List<Product>();
            allProduct = context.Products.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ProductReport.rpt"));

            rd.SetDataSource(allProduct);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ProductList.pdf");
        }
    }
}