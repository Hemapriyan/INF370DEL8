using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class mvcClientFeedbackModel
    {

        public int ClientFeedBackID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Profesionalism { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Cleanliness { get; set; }

        public Nullable<int> Friendliness { get; set; }
        public Nullable<int> Punctuality { get; set; }
        public Nullable<int> ServiceQuality { get; set; }
        public int ClientID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Comment { get; set; }
        public int ServiceRequestID { get; set; }

        public virtual mvcServiceRequestModel ServiceRequest { get; set; }
    }
}