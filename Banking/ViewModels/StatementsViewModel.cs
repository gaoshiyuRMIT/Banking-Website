using System;
using System.Collections.Generic;

using Banking.Models;

namespace Banking.ViewModels
{
    public class StatementsViewModel
    {
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public int Page { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
