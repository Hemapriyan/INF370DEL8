using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class ClientModelGlobal
    {
        
        public mvcClientModel emp { get; set; }
       
        public User user { get; set; }
        public Location location { get; set; }

        public string UserEmail { get; set; }
        public int UserRoleId { get; set; }
    }
}