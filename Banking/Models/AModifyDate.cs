using System;
namespace Banking.Models
{
    public abstract class AModifyDate
    {
        public DateTime ModifyDate { get; set; }
        public DateTime ModifyDateLocal => ModifyDate.ToLocalTime();
    }
}
