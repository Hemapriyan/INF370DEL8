using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class mvcServiceRequestModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcServiceRequestModel()
        {
            this.ClientFeedbacks = new HashSet<mvcClientFeedbackModel>();
            this.Complaints = new HashSet<mvcComplaintModel>();
            this.EmployeeRequests = new HashSet<mvcEmployeeRequestModel>();
            this.Services = new HashSet<mvcServiceModel>();
        }

        public int ServiceRequestID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use Letters only please")]
        [DataType(DataType.Text)]
        public string BusinessName { get; set; }
        public int ServiceRequestStatusID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.DateTime)]
        public System.DateTime ServiceRequestDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.DateTime)]
        public System.DateTime ServiceBookedDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public int LocationID { get; set; }
        public Nullable<System.DateTime> ServiceRequestEndDate { get; set; }
        public Nullable<bool> IsFullDay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcClientFeedbackModel> ClientFeedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcComplaintModel> Complaints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcEmployeeRequestModel> EmployeeRequests { get; set; }
        public virtual mvcLocationModel Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcServiceModel> Services { get; set; }
        public virtual mvcServiceRequestStatusModel ServiceRequestStatu { get; set; }
    }

}