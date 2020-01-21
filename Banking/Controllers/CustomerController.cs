using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Banking.Models;
using Banking.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BankingContext _context;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public CustomerController(BankingContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);

            return View(customer);
        }

        public IActionResult Withdraw()
        {
            return View();
        }

        public IActionResult Deposit()
        {
            return View();
        }

        public IActionResult Transfer()
        {
            return View();
        }

        public IActionResult Statements()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
