using System;
using Banking.Models;

namespace Banking.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
        }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string TFN { get; set; }
    }
}
