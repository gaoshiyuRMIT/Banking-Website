using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using Banking.Attributes;
using Banking.Data;
using Banking.Models;
using Banking.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.Controllers
{

    [AuthorizeCustomer]
    [Route("Customer/BillPay/Payee")]
    public class PayeeController : Controller
    {
        private readonly BankingContext _context;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public PayeeController(BankingContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var payees = from account in _context.Account
                         join billPay in _context.BillPay
                         on account.AccountNumber equals billPay.AccountNumber
                         where account.CustomerID == CustomerID
                         select billPay.Payee;
            payees = payees.Distinct<Payee>().OrderBy<Payee, string>(x => x.Name);
            return View(payees);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int payeeID)
        {
            var payee = await _context.Payee.FindAsync(payeeID);

            var viewModel = BillPaySessionKey.GetEditViewModelFromSession(HttpContext.Session);
            viewModel.PayeeID = payeeID;
            BillPaySessionKey.SetEditViewModelToSession(viewModel, HttpContext.Session);

            if (viewModel.BillPayEditOp == BillPayEditOp.Create)
                return RedirectToAction("Create", "BillPay");
            return RedirectToAction("Edit", "BillPay");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Address,City,State,PostCode,Phone")] Payee payee)
        {
            await _context.Payee.AddAsync(payee);
            await _context.SaveChangesAsync();

            var viewModel = BillPaySessionKey.GetEditViewModelFromSession(HttpContext.Session);
            viewModel.PayeeID = payee.PayeeID;
            BillPaySessionKey.SetEditViewModelToSession(viewModel, HttpContext.Session);

            if (viewModel.BillPayEditOp == BillPayEditOp.Create)
                return RedirectToAction("Create", "BillPay");
            return RedirectToAction("Edit", "BillPay");
        }
    }
}
