using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class mvcEmployeeRequestModel
    {

        public int EmployeeID { get; set; }
        public int ServiceRequestID { get; set; }

        public System.DateTime DateAssigned { get; set; }

        public virtual mvcEmployeeModel Employee { get; set; }
        public virtual mvcServiceRequestModel ServiceRequest { get; set; }
    }
}