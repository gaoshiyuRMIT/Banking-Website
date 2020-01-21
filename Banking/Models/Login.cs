using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Login
    {
        [Key]
        [StringLength(8)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
