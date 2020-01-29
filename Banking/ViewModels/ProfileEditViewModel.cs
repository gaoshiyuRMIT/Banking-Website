using System;
using System.ComponentModel.DataAnnotations;

using Banking.Models;

namespace Banking.ViewModels
{
    public class ProfileEditViewModel : Customer
    {
        [Required, StringLength(20)]
        public string Password { get; set; }

        public static ProfileEditViewModel FromCustomer(Customer c) {
            return new ProfileEditViewModel 
            {
                Name = c.Name,
                Address = c.Address,
                City = c.City,
                State = c.State,
                PostCode = c.PostCode,
                Phone = c.Phone,
                TFN = c.TFN
            };
        }
    }
}
