using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Banking.Models
{
    public enum BillPayPeriod
    {
        OnceOff = 0,
        Monthly = 1,
        Quarterly = 2,
        Annually = 3
    };

    public class BillPay
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
        public DateTime ModifyDate { get; set; }
       // public string Comment { get; set; }
    }

}
