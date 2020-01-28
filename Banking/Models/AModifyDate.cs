using System;
using System.ComponentModel.DataAnnotations;
namespace Banking.Models

{
    public abstract class AModifyDate
    {
        [Display(Name = "Modify Date")]
        [Required, StringLength(8, ErrorMessage = "No More Than 8 digits")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ModifyDate { get; set; }
        public DateTime ModifyDateLocal => ModifyDate.ToLocalTime();
    }
}
