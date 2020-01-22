using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public enum TransactionType : int
    {
        Deposit = 0,
        Withdrawal = 1,
        Transfer = 2,
        ServiceCharge = 3,
        BillPay = 4
    }

    public class Transaction
    {
        public int TransactionID { get; set; }
        public TransactionType TransactionType { get; set; }

        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        public int? DestAccountNumber { get; set; }
        public virtual Account DestAccount { get; set; }

        public decimal Amount { get; set; }
        public string Comment { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
