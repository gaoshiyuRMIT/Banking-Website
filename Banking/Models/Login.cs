using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Login : AModifyDate
    {
        [Key]
        [StringLength(8)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
