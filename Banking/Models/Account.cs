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

        [Range(0,9999, ErrorMessage = "No such account")]
        [Required(AllowEmptyStrings = false)]
        public int AccountNumber { get; set; }


        public AccountType AccountType { get; set; }

        [Range(0,9999, ErrorMessage = "No such Customer")]
        [Required(AllowEmptyStrings = false)]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }


        public decimal Balance { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(AllowEmptyStrings = false)]
        public DateTime ModifyDate { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<BillPay> BillPays { get; set; }
    }
}
