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
    using System.ComponentModel.DataAnnotations;

    public partial class mvcLoginModel
    {
        public int LoginID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }
        public Nullable<System.TimeSpan> LoginTime { get; set; }
    
        public virtual mvcUserModel User { get; set; }
    }
}
