using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Banking.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name = "Customer ID")]
        [Range(0, 9999, ErrorMessage = "Please enter 4 characters")]
        [Required(ErrorMessage = "Not Allow Null")]
        public int CustomerID { get; set; }

        [StringLength(50, ErrorMessage = "Please enter 50 characters")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required, StringLength(11, ErrorMessage = "Please enter 11 characters")]

        public string TFN { get; set; }

        [StringLength(50,ErrorMessage = "Please enter less than 50 characters")]
        public string Address { get; set; }

        [StringLength(40, ErrorMessage = "Please enter less than 40 characters")]
        public string City { get; set; }

        //[Range(0, 20, ErrorMessage = "Please enter less than 40 characters")]
        [StringLength(3, ErrorMessage = "Must Be 3 Lettered Austrilian State")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only  letter allowed.")]
        public string State { get; set; }

        [Display(Name = "Post Code")]
        //[Range(0, 10, ErrorMessage = "Please enter less than 10 characters")]
        [StringLength(4, ErrorMessage = "Please enter 4 digit number")]
        [RegularExpression(@"^\d{4}", ErrorMessage = "Only  Numbers allowed.")]
        public string PostCode { get; set; }

        //[StringLength(10, ErrorMessage = "Please enter the format:(61)-XXXXXXXXX")]
        //[DisplayFormat(PhoneFormatString = "{0:(61)-XXXXXXXX}"0]
        [Required(AllowEmptyStrings = false)]
        public string Phone { get; set; }

        public virtual List<Account> Accounts { get; set; }
        public IEnumerable<Payee> PayeeHistory
        {
            get
            {
                List<Payee> payees = new List<Payee>();
                Accounts.ForEach(a =>
                    a.BillPays.ForEach(bp =>
                        payees.Add(bp.Payee)));
                return payees.Distinct<Payee>();
            }
        }
    }
}
