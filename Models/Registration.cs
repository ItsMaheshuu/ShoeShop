//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMEProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Registration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Registration()
        {
            this.Orders = new HashSet<Order>();
        }
        [DisplayName("Customer Id")]
        [Required(ErrorMessage = "customer id is required field")]
        public int customer_id { get; set; }

        [DisplayName("Firstname")]
        [Required(ErrorMessage = "Firstname is required field")]
        public string Firstname { get; set; }

        [DisplayName("Laststname")]
        [Required(ErrorMessage = "Laststname is required field")]
        public string Lastname { get; set; }

        [DisplayName("Phone No")]
        [Required(ErrorMessage = "Phone No is required field")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]

        public string Phone_no { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required field")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required field")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email.")]

        public string Email { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required field")]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
