﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Banking.Models
{
    public class Payee : APerson
    {

        [Display(Name = "Payee ID")]
        [Required, Range(0,9999, ErrorMessage = "No More Than 4 digits")]
        [RegularExpression(@"^\d{4}", ErrorMessage = "Only Numbers Allowed")]
        public int PayeeID { get; set; }
        public string PayeeIDStr => PayeeID.ToString().PadLeft(4, '0');

        public override bool Equals(object obj)
        {
            Payee other = obj as Payee;
            return other != null && other.PayeeID == PayeeID;
        }

        public override int GetHashCode()
        {
            return PayeeID;
        }
    }
}
