using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class CustomiseClass
    {
        public List<Employee> EmployeeList { get; set; }
        public List<EmployeeType> EmployeeTypeList { get; set; }
        public Employee emp { get; set; }

        public List<Product> ProductList { get; set; }

        public List<ProductStatu> ProductStatusList { get; set; }

        public Product pd { get; set; }
        public Client c { get; set; }
       
        public User u { get; set; }

    }
}