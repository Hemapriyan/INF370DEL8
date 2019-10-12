//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Snow_System
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class ServiceRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceRequest()
        {
            this.ClientFeedbacks = new HashSet<ClientFeedback>();
            this.Complaints = new HashSet<Complaint>();
            this.EmployeeRequests = new HashSet<EmployeeRequest>();
            this.Services = new HashSet<Service>();
        }
    
        public int ServiceRequestID { get; set; }
        [DisplayName("Service Request Status")]
        public int ServiceRequestStatusID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        [DisplayName("Service Request Date")]
      
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        
        public System.DateTime ServiceRequestDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayName("Service Booked Date")]
        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
      
        public System.DateTime ServiceBookedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DisplayName("Service Booked End Date")]
        public Nullable<System.DateTime> ServiceBookedEndDate { get; set; }
        
        [DisplayName("Comment")]
        public string Comment { get; set; }
        public int LocationID { get; set; }
        public Nullable<bool> IsFullDay { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientFeedback> ClientFeedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual Location Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
        public virtual ServiceRequestStatu ServiceRequestStatu { get; set; }
    }
}
