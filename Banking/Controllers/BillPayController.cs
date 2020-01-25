﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Index()
        {
            IEnumerable<BillPay> billPays = _context.BillPay
                .Where(x => x.Account.CustomerID == CustomerID)
                .OrderBy<BillPay, DateTime>(x => x.ScheduleDate);
            return View(billPays);
        }

        public async Task<IActionResult> Edit(int billPayId)
        {
            var viewModel = BillPaySessionKey.GetEditViewModelFromSession(HttpContext.Session);
            if (viewModel != null)
                return View(viewModel);

            var billPay = await _context.BillPay.FindAsync(billPayId);

            if (billPay == null)
                return NotFound();

            HttpContext.Session.SetInt32(BillPaySessionKey.EditBillPayID, billPayId);

            viewModel = new BillPayEditViewModel
            {
                AccountType = billPay.Account.AccountType,
                ScheduleDateLocal = billPay.ScheduleDate.ToLocalTime(),
                Period = billPay.Period,
                BillPayEditOp = BillPayEditOp.Edit
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = BillPaySessionKey.GetEditViewModelFromSession(HttpContext.Session);
            if (viewModel != null)
                return View(viewModel);

            return View(new BillPayEditViewModel
            {
                BillPayEditOp = BillPayEditOp.Create
            });
        }

        [HttpPost]
        public IActionResult EditOrCreateToPayee(BillPayEditViewModel viewModel)
        {
            BillPaySessionKey.SetEditViewModelToSession(viewModel, HttpContext.Session);

            if (viewModel.PayeeOp == PayeeOp.Choose)
                return RedirectToAction("Index", "Payee");
            return RedirectToAction("Create", "Payee");
        }

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
                PayeeID = viewModel.PayeeID
            };
            _context.BillPay.Update(billPay);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove(BillPaySessionKey.EditBillPayID);
            HttpContext.Session.Remove(BillPaySessionKey.EditOrCreate);

            return RedirectToAction("Index");
        }

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
                PayeeID = viewModel.PayeeID,
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
