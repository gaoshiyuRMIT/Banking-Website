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

        [Display(Name = "Transaction ID")]
        [Range(0, 9999, ErrorMessage = "No such account")]
        [Required(ErrorMessage = "Not Allow Null")]
        public int TransactionID { get; set; }

        [Display(Name = "Transaction Type")]
        [Required(AllowEmptyStrings = false)]
        public TransactionType TransactionType { get; set; }


        [Display(Name = "Account Number")]
        [Range(0, 9999, ErrorMessage = "No such account")]
        [Required(ErrorMessage = "Not Allow Null")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }


        [Display(Name = "Dest Account Number")]
        [Range(0, 9999, ErrorMessage = "No such account")]
        public int? DestAccountNumber { get; set; }
        public virtual Account DestAccount { get; set; }


        [DataType(DataType.Currency)]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount { get; set; }


        [StringLength(255, ErrorMessage = "Please enter 255 characters")]
        public string Comment { get; set; }


        [Display(Name = "Modify Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
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
