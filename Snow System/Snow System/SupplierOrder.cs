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
    
    public partial class SupplierOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierOrder()
        {
            this.SupplierOrderLines = new HashSet<SupplierOrderLine>();
        }
    
        public int SupplierOrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int SupplierStatusID { get; set; }
        public int SupplierID { get; set; }
    
        public virtual Supplier Supplier { get; set; }
        public virtual SupplierStatu SupplierStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierOrderLine> SupplierOrderLines { get; set; }
    }
}
