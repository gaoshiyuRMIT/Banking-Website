using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Banking.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string TFN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }

        public virtual List<Account> Accounts { get; set; }
        public IEnumerable<Payee> PayeeHistory
        {
            get
            {
                List<Payee> payees = new List<Payee>();
                Accounts.ForEach(a =>
                    a.BillPays.ForEach(bp =>
                        payees.Add(bp.Payee)));
                return payees.Distinct<Payee>();
            }
        }
    }
}
