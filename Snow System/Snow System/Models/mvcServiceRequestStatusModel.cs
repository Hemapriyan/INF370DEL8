using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class mvcServiceRequestStatusModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcServiceRequestStatusModel()
        {
            this.ServiceRequests = new HashSet<mvcServiceRequestModel>();
        }

        public int ServiceRequestStatusID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcServiceRequestModel> ServiceRequests { get; set; }
    }
}