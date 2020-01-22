using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Collections.Generic;

using Banking.Models;

namespace Banking.ViewModels
{
    public enum OperationStatus
    {
        Pending = 0,
        Successful = 1
    }

    public class BasicOpViewModel
    {
        private List<SelectListItem> _accountTypes = new List<SelectListItem>();
        private Account _account;
        private AccountType _accountType;
        private Customer _customer;

        public Customer Customer {
            get => _customer;
            set
            {
                _customer = value;
                // account & account type list needs to update
                _accountTypes.Clear();
                _account = null;
            }
        }
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
            set => _accountTypes = value;
        }
        // selected
        public AccountType AccountType {
            get => _accountType;
            set
            {
                _accountType = value;
                // account needs to update
                _account = null;
            }
        }
        public Account Account
        {
            set => _account = value;
            get
            {
                if (_account == null && Customer != null)
                {
                    return Customer.Accounts.Find(x => x.AccountType == AccountType);
                }
                return _account;
            }
        }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public OperationStatus OperationStatus { get; set; }
            = OperationStatus.Pending;

        public virtual void Validate(ModelStateDictionary modelState)
        {
            if (Amount <= 0)
                modelState.AddModelError("Amount", "Amount must be greater than zero.");
        }
    }
}
