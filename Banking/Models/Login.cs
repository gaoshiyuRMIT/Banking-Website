using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Login : AModifyDate
    {


        [Key]
        [Display(Name = "User ID")]
        [Required, StringLength(8, ErrorMessage = "No More Than 8 digits")]
        [RegularExpression(@"^\d{8}", ErrorMessage = "Only Numbers Allowed")]

        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }



        [Display(Name = "Customer ID")]
        [Required,Range(0,9999, ErrorMessage ="No More Than 4 digits")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

