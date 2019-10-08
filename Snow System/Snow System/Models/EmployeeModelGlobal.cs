using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class EmployeeModelGlobal
    {
        public List<EmployeeType> EmployeeTypeList { get; set; }
        public mvcEmployeeModel emp { get; set; }
        public User user { get; set; }

    }
}