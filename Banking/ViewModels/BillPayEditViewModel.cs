﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Banking.Models;

namespace Banking.ViewModels
{
    public enum BillPayEditOp
    {
        Create = 0,
        Edit = 1
    }

    public enum PayeeOp
    {
        Choose = 0,
        Create = 1
    }

    public class BillPayEditViewModel : UpdateOpViewModel
    {
        public BillPayPeriod Period { get; set; }
        public List<SelectListItem> PeriodSelect
        {
            get
            {
                List<SelectListItem> select = new List<SelectListItem>();
                foreach (var name in Enum.GetNames(typeof(BillPayPeriod)))
                {
                    object bp = Enum.Parse(typeof(BillPayPeriod), name);
                    select.Add(new SelectListItem
                    {
                        Text = bp.ToString(),
                        Value = name
                    });
                }
                return select;
            }
        }
        public DateTime ScheduleDateLocal { get; set; }
        public DateTime ScheduleDate => ScheduleDateLocal.ToUniversalTime();
        public Payee Payee { get; set; }

        public override void Validate(ModelStateDictionary modelState)
        {
            base.Validate(modelState);

            if (ScheduleDate <= DateTime.UtcNow)
                modelState.AddModelError("ScheduleDateLocal",
                    "Schedule date must be in the future.");
        }
        public BillPayEditOp BillPayEditOp { get; set; }
        public PayeeOp PayeeOp { get; set; }
    }
}
