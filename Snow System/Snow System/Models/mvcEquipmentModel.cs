//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Snow_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class mvcEquipmentModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcEquipmentModel()
        {
            this.CheckEquipmentLines = new HashSet<mvcCheckEquipmentLineModel>();
        }
    
        public int EquipmentID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Quantity")]
        [Required(ErrorMessage = "This field is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime PurchaseDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcCheckEquipmentLineModel> CheckEquipmentLines { get; set; }
        
    }
}
