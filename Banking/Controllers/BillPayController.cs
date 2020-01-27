using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


using Banking.Attributes;
using Banking.Data;
using Banking.Models;
using Banking.ViewModels;

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
        public IActionResult Index(int discard = 0)
        {
            if (discard == 1)
            {
                HttpContext.Session.Remove(BillPaySessionKey.EditBillPayID);
                HttpContext.Session.Remove(BillPaySessionKey.EditOrCreate);
            }
            IEnumerable<BillPay> billPays = _context.BillPay
                .Where(x => x.Account.CustomerID == CustomerID)
                .OrderBy<BillPay, DateTime>(x => x.ScheduleDate);
            return View(billPays);
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(int billPayId)
        {
            var viewModel = BillPaySessionKey.GetEditViewModelFromSession(HttpContext.Session);

            if (viewModel == null)
            {
                var billPay = await _context.BillPay.FindAsync(billPayId);

                if (billPay == null)
                    return NotFound();

                viewModel = new BillPayEditViewModel
                {
                    AccountType = billPay.Account.AccountType,
                    ScheduleDateLocal = billPay.ScheduleDate.ToLocalTime(),
                    Period = billPay.Period,
                    BillPayEditOp = BillPayEditOp.Edit,
                    Amount = billPay.Amount
                };

                HttpContext.Session.SetInt32(BillPaySessionKey.EditBillPayID, billPayId);
            }

            var customer = await _context.Customer.FindAsync(CustomerID);
            viewModel.Customer = customer;

            return View(viewModel);
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var viewModel = BillPaySessionKey.GetEditViewModelFromSession(HttpContext.Session);
            if (viewModel == null)
                viewModel = new BillPayEditViewModel();

            var customer = await _context.Customer.FindAsync(CustomerID);

            viewModel.BillPayEditOp = BillPayEditOp.Create;
            viewModel.Customer = customer;
            viewModel.ScheduleDate = DateTime.UtcNow;

            return View(viewModel);
        }

        [Route("EditOrCreateToPayee/Create")]
        [HttpPost]
        public IActionResult EditOrCreateToPayeeCreate(BillPayEditViewModel viewModel)
        {                
            BillPaySessionKey.SetEditViewModelToSession(viewModel, HttpContext.Session);

            return RedirectToAction("Create", "Payee");
        }

        [Route("EditOrCreateToPayee")]
        [HttpPost]
        public IActionResult EditOrCreateToPayee(BillPayEditViewModel viewModel)
        {
            BillPaySessionKey.SetEditViewModelToSession(viewModel, HttpContext.Session);

            return RedirectToAction("Index", "Payee");
        }


        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(BillPayEditViewModel viewModel)
        {
            int? billPayID = HttpContext.Session.GetInt32(BillPaySessionKey.EditBillPayID);
            if (billPayID == null)
                return RedirectToAction("Index");

            var customer = await _context.Customer.FindAsync(CustomerID);
            viewModel.Customer = customer;

            viewModel.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(viewModel);

            BillPay billPay = new BillPay
            {
                BillPayID = billPayID.Value,
                AccountNumber = viewModel.Account.AccountNumber,
                Period = viewModel.Period,
                Amount = viewModel.Amount,
                ScheduleDate = viewModel.ScheduleDate,
                Payee = viewModel.Payee
            };
            _context.BillPay.Update(billPay);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove(BillPaySessionKey.EditBillPayID);
            HttpContext.Session.Remove(BillPaySessionKey.EditOrCreate);

            return RedirectToAction("Index");
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(BillPayEditViewModel viewModel)
        {
            viewModel.Customer = await _context.Customer.FindAsync(CustomerID);

            viewModel.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(viewModel);

            var billPay = new BillPay
            {
                Amount = viewModel.Amount,
                Payee = viewModel.Payee,
                Period = viewModel.Period,
                ScheduleDate = viewModel.ScheduleDate
            };
            viewModel.Account.BillPays.Add(billPay);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove(BillPaySessionKey.EditBillPayID);
            HttpContext.Session.Remove(BillPaySessionKey.EditOrCreate);

            return RedirectToAction("Index");
        }
    }
}
