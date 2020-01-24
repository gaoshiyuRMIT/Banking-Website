using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Login
    {
        [Key]
        [StringLength(8)]
        [Display(Name = "User ID")]
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Not a number"), StringLength(8)]
        //[RegularExpression("[^0-9]", ErrorMessage = "User ID must be numeric"), StringLength(8)]
        [RegularExpression(@"^\d{8}", ErrorMessage = "Only  Numbers allowed.")]
        [Required(AllowEmptyStrings = false)]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [StringLength(4)]
        [Required(AllowEmptyStrings = false)]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(AllowEmptyStrings = false)]
        public DateTime ModifyDate { get; set; }
    }
}
