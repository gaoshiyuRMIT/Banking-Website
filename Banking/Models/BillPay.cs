using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Banking.Extensions;

namespace Banking.Models
{
    public enum BillPayPeriod
    {
        OnceOff = 0,
        Monthly = 1,
        Quarterly = 2,
        Annually = 3
    };

    public class BillPay : AModifyDate
    {
        public int BillPayID { get; set; }
        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        public decimal Amount { get; set; }
        public DateTime ScheduleDate { get; set; }
        public BillPayPeriod Period { get; set; }
        public string Comment { get; set; }

        public DateTime ScheduleDateLocal => ScheduleDate.ToLocalTime();
        
        public DateTime? NextDateTime
        {
            get
            {
                if (ScheduleDate > DateTime.UtcNow)
                    return ScheduleDate;
                if (Period == BillPayPeriod.OnceOff)
                    return null;
                int nMonth = Period.ToMonth().Value;
                DateTime _scheduleDate = ScheduleDate;
                while (_scheduleDate <= DateTime.UtcNow)
                {
                    _scheduleDate.AddMonths(nMonth);
                }
                return _scheduleDate;
            }
        }

        public bool ExecuteBillPay(out string errMsg)
        {
            errMsg = string.Empty;
            if (Account.Balance - Amount < Account.MinBalance)
            {
                errMsg = "The amount after deduction would be lower than the minimum allowed.";
                return false;
            }
            Account.Balance -= Amount;
            Account.Transactions.Add(new Transaction
            {
                TransactionType = TransactionType.BillPay,
                Amount = Amount,
                ModifyDate = DateTime.UtcNow
            });
            // update schedule date
            if (Period != BillPayPeriod.OnceOff)
                ScheduleDate = NextDateTime.Value;
            return true;
        }
    }

}
