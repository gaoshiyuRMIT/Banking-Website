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
        public decimal MinBalance {
            get => AccountType == AccountType.Savings ? 0 : 200;
        }
        public decimal MinOpeningBalance
        {
            get => AccountType == AccountType.Savings ? 100 : 500;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal Balance { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<BillPay> BillPays { get; set; }
    }
}
