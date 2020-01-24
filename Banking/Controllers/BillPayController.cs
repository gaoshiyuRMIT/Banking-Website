using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Banking.Attributes;
using Banking.Data;
using Banking.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.Controllers
{
    [AuthorizeCustomer]
    [Route("Customer/BillPay")]
    public class BillPayController : Controller
    {
        private readonly BankingContext _context;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public BillPayController(BankingContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
