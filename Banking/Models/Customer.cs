using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Banking.Models
{
    public class Customer : APerson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        public string TFN { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }
}
