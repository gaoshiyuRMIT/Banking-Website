using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Banking.ViewModels
{
    public class WithdrawViewModel : BasicOpViewModel
    {
        public override void Validate(ModelStateDictionary modelState)
        {
            base.Validate(modelState);
            if (Amount > Account.Balance)
                modelState.AddModelError("Amount", "Amount exceeds current balance.");
        }
    }
}
