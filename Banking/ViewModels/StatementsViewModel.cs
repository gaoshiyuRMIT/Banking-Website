using System;
using System.Collections.Generic;
using X.PagedList;

using Banking.Models;

namespace Banking.ViewModels
{
    public class StatementsViewModel : BasicOpViewModel
    {
        public IPagedList<Transaction> Transactions { get; set; }
    }
}
