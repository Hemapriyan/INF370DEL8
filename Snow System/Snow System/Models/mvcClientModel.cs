using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class mvcClientModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcClientModel()
        {
            this.CompanyInfoes = new HashSet<CompanyInfo>();
            this.Complaints = new HashSet<Complaint>();
            this.Locations = new HashSet<Location>();
            this.ProductOrders = new HashSet<ProductOrder>();
        }


        public int ClientID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Client Name")]
        [DataType(DataType.Text)]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [DisplayName("Client Surname")]
        public string ClientSurname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName(" Email Address")]
        [DataType(DataType.EmailAddress)]

        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("House Address")]
        

        public string HouseAddress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Client Type")]

        public int ClientTypeID { get; set; }
        public int LocationTypeID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyInfo> CompanyInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location> Locations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public List<Client> ClientList { get; set; }
        
        public Client client { get; set; }
        public int LocationID { get; set; }
        public string StreetAddress { get; set; }


        public string Suburb { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("City")]
        public string City { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Contact Name")]
        public string ContactPersonName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Contact Number")]
        public string ContactPersonNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]

        //user
        
        
        [DisplayName(" Email Addresss")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}