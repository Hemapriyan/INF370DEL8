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

    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.CheckEquipments = new HashSet<CheckEquipment>();
        }
    
        public int VehicleID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [DisplayName("Vehicle Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [DisplayName("License Number")]
        [MaxLength(8)]
        [MinLength(8)]

        public string LicenseNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DisplayName("Purhase Date")]
        public System.DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Vehicle Type")]
        public int VehicleTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckEquipment> CheckEquipments { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public List<VehicleType> VehicleTypeList { get; set; }
    }
}
