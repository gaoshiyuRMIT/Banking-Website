using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public enum AccountType : int
    {
        Savings = 0,
        Checking = 1
    }

    public class Account
    {
        public decimal MinBalance {
            get => AccountType == AccountType.Savings ? 0 : 200;
        }
        public decimal MinOpeningBalance
        {
            get => AccountType == AccountType.Savings ? 100 : 500;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name ="Account Number")]
        [Range(0,9999, ErrorMessage = "No more than 4")]
        [Required(ErrorMessage = "Not Allow Null")]
        public int AccountNumber { get; set; }

        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }


        [Display(Name = "Customer ID")]
        [Range(0,9999, ErrorMessage = "No such Customer")]
        [Required(ErrorMessage = "Not Allow Null")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }


        public decimal Balance { get; set; }

        [Display(Name = "Modify Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(AllowEmptyStrings = false)]
        public DateTime ModifyDate { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<BillPay> BillPays { get; set; }
    }
}
