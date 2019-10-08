using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class LocationModelGlobal
    {
        public List<LocationType> LocationTypeList { get; set; }
        public List<Client> ClientLocList { get; set; }
        public mvcLocationModel emp { get; set; }
        public LocationType locationtype { get; set; }
        public Client client { get; set; }
    }
}