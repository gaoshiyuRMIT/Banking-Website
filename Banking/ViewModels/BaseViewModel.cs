using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Banking.ViewModels
{
    public class BaseViewModel
    {
        public virtual void Validate(ModelStateDictionary modelState)
        {
        }

        public virtual void Clear()
        {
        }

    }
}
