using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class AuditLogController : Controller
    {
        // GET: AuditLog

        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "UserID")
            {
                return View(db.AuditLogs.Where(x => x.User.UserEmail== search || search == null).ToList());
            }
            else if (searchBy == "Audit Log Type") {
                return View(db.AuditLogs.Where(x => x.AuditLogType.Description == search || search == null).ToList());
            }
            else
            {
                return View(db.AuditLogs.Where(x => x.DateAccessed.ToString().StartsWith(search) || search == null).ToList());
            }
        }


    }
}
