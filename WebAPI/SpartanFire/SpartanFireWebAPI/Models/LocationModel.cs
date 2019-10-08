using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpartanFireWebAPI.Models
{
    public class LocationModel
    {
        public Location emp { get; set; }
        public Client client { get; set; }
    }
}