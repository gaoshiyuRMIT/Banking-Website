using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Banking.Models
{
    public abstract class APerson
    {
        [Display(Name = "Name")]
        [Required, StringLength(50, ErrorMessage = "Please enter 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only  letter allowed.")]
        public string Name { get; set; }


        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Please enter no more than 50 characters")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [StringLength(40, ErrorMessage = "Please enter no more than 40 characters")]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(3, MinimumLength = 3,ErrorMessage = "Please enter 3 lettered Australian state")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only 3 letters allowed.")]
        public string State { get; set; }


        [Display(Name = "Post Code")]
        [StringLength(4,  MinimumLength = 4,ErrorMessage = "Please enter 4 digit number")]
        [RegularExpression(@"^\d{4}", ErrorMessage = "Only 4 Numbers allowed.")]
        public string PostCode { get; set; }


        [Display(Name = "Phone")]
        //[Required(ErrorMessage = "You must provide a phone number")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(\+61)[-. ]([0-9]{9})$", ErrorMessage = "Not a valid phone number")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:61-XXXXXXXXX}")]
        //[RegularExpression(@"^\(\+61|0)[0-9]{9}$", ErrorMessage = "Not a valid phone number, the format must be 61XXXXXXXXX)]
        
        public string Phone { get; set; }
    }
}
