using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Banking.ViewModels
{
    public class TransferViewModel : BasicOpViewModel
    {
        public int DestAccountNumber { get; set; }

        public override void Validate(ModelStateDictionary modelState)
        {
            base.Validate(modelState);
            if (Account.Balance - Amount < Account.MinBalance)
                modelState.AddModelError("Amount",
                    "balance would be lower than the allowed minimum after deduction.");
            if (Account.AccountNumber == DestAccountNumber)
                modelState.AddModelError("DestAccountNumber",
                    "Cannot transfer from and to the same account.");
        }

    }
}
