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

    public class Account : AModifyDate
    {
        [Display(Name = "Min Balance")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Saving Account Must Be Zero and more")]
        public decimal MinBalance {
            get => AccountType == AccountType.Savings ? 0 : 200;
        }
        [Display(Name = "Min Opening Balance")]
        [Range(200, (double)decimal.MaxValue, ErrorMessage = "Saving Account Opening Balance Must Be More Than $200")]
        public decimal MinOpeningBalance
        {
            get => AccountType == AccountType.Savings ? 100 : 500;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name = "Account Number")]
        [Required, Range(0, 9999, ErrorMessage = "No More Than 4 digits")]
        public int AccountNumber { get; set; }


        [Display(Name = "Account Type")]
        [Required,StringLength(1,ErrorMessage ="No Such Account")]
        public AccountType AccountType { get; set; }


        [Display(Name = "Customer ID")]
        [Required, Range(0, 9999, ErrorMessage = "No More Than 4 digits")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        
           
        [Required, Range(0d, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Balance { get; set; }


        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<BillPay> BillPays { get; set; }
    }
}
