using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snow_System.Models
{
    public class mvcEmployeeModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcEmployeeModel()
        {
            this.AssignDeliveries = new HashSet<mvcAssignDeliveryModel>();
            this.EmployeeRequests = new HashSet<mvcEmployeeRequestModel>();
        }

        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string IDNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string HouseAddress { get; set; }
        [DisplayName("Upload Image")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Certification { get; set; }
        [Display(Name = "Employee Type ID")]
        public int EmployeeTypeID { get; set; }
        public int UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcAssignDeliveryModel> AssignDeliveries { get; set; }
        public virtual  mvcEmployeeTypeModel EmployeeType { get; set; }
        public virtual mvcUserModel User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcEmployeeRequestModel> EmployeeRequests { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public List<EmployeeType> EmployeeTypeList { get; set; }
        public Employee emp { get; set; }

        //usern only for user tABLE

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("User Email")]
        public string UserName { get; set; }
       
    }
}