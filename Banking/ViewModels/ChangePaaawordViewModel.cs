using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleHashing;
namespace Banking.ViewModels
{
    public class ChangePaaawordViewModel : LoginViewModel
    {
        // public ChangePaaawordViewModel()
        //{
        // }

        [Required, StringLength(20)]
        public string Password { get; set; }
        

        [Required, StringLength(20)]
        public string ChangePassword { get; set; }
    }
}
