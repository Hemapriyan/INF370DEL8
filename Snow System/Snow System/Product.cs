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

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ProductOrderLines = new HashSet<ProductOrderLine>();
            this.ServiceLines = new HashSet<ServiceLine>();
            this.SupplierOrderLines = new HashSet<SupplierOrderLine>();
            this.WriteOfProducts = new HashSet<WriteOfProduct>();
        }

        public int ProductID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [DisplayName("Product Description")]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "This field is required")]

        [DisplayName("Quantity on Hand")]
        public int QuantityOnHand { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Currency)]
        [DisplayName("Selling Price")]
        public double SellingPrice { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Product Status")]
        public int ProductStatusID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Product Type")]
        public int ProductTypeID { get; set; }
        [Required(ErrorMessage = "This field is required")]

        [DisplayName("Minimum Quantity")]
        public int MinimumQuantity { get; set; }

        public virtual ProductStatu ProductStatu { get; set; }
        public virtual ProductType ProductType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrderLine> ProductOrderLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceLine> ServiceLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierOrderLine> SupplierOrderLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WriteOfProduct> WriteOfProducts { get; set; }

        public List<ProductType> ProductTypeList { get; set; }
        public List<ProductStatu> ProductStatusList { get; set; }
    }
}
