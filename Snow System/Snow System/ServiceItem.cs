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

    public partial class ServiceItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceItem()
        {
            this.Services = new HashSet<Service>();
        }
    
        public int ServiceItemID { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Item Number")]
        public int itemNumber { get; set; }
        public int WorkDoneID { get; set; }
        public int ServiceItemTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
        public virtual ServiceItemType ServiceItemType { get; set; }
        public virtual WorkDone WorkDone { get; set; }
    }
}
