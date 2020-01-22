using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

using Banking.Models;

namespace Banking.ViewModels
{
    public class BasicOpViewModel
    {
        private List<SelectListItem> _accountTypes = new List<SelectListItem>();
        public Customer Customer { get; set; }
        public List<SelectListItem> AccountTypes {
            get {
                if (_accountTypes.Count == 0 && Customer != null)
                {
                    var accountTypes = from account in Customer.Accounts
                                       select account.AccountType;
                    foreach (var type in accountTypes)
                        _accountTypes.Add(new SelectListItem
                        {
                            Text = type.ToString(),
                            Value = type.ToString()
                        });
                }
                return _accountTypes;
            }
            set => _accountTypes = AccountTypes;
        }
        // selected
        public AccountType AccountType { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
