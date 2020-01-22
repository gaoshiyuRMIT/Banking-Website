using System;
using System.Collections.Generic;
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
        public const int NFreeTransaction = 4;
        public static readonly Dictionary<TransactionType, decimal> ServiceFee =
            new Dictionary<TransactionType, decimal>
            {
                [TransactionType.Withdrawal] = 0.1M,
                [TransactionType.Transfer] = 0.2M
            };
        public bool ShouldCharge {
            get => TransactionType == TransactionType.Transfer
                || TransactionType == TransactionType.Withdrawal;
        }

        public int TransactionID { get; set; }
        public TransactionType TransactionType { get; set; }

        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        public int? DestAccountNumber { get; set; }
        public virtual Account DestAccount { get; set; }

        public decimal Amount { get; set; }
        public string Comment { get; set; }

        public DateTime ModifyDate { get; set; }

        public Transaction CreateServiceTransaction()
        {
            if (!ShouldCharge)
                return null;
            return new Transaction
            {
                TransactionType = TransactionType.ServiceCharge,
                Amount = ServiceFee[TransactionType],
                Comment = Comment + " service charge",
                DestAccount = DestAccount,
                ModifyDate = ModifyDate
            };
        }
    }
}
