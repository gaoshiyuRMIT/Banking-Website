using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Banking.Models;
using Banking.ViewModels;
using Banking.Data;
using Banking.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.Controllers
{
    [AuthorizeCustomer]
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

        public async Task<IActionResult> Withdraw()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            var viewModel = new WithdrawViewModel
            {
                Customer = customer
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(WithdrawViewModel viewModel)
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            viewModel.Customer = customer;

            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Amount must be greater than zero.");
                viewModel.OperationStatus = OperationStatus.Failed;
                return View(viewModel);
            }

            var account = customer.Accounts.Find(x => x.AccountType == viewModel.AccountType);
            if (viewModel.Amount > account.Balance)
            {
                ModelState.AddModelError("Amount", "Amount exceeds current balance.");
                viewModel.OperationStatus = OperationStatus.Failed;
                return View(viewModel);
            }

            account.Balance -= viewModel.Amount;
            account.Transactions.Add(new Transaction
            {
                TransactionType = TransactionType.Deposit,
                Amount = viewModel.Amount,
                ModifyDate = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            viewModel.OperationStatus = OperationStatus.Successful;
            viewModel.Amount = 0;
            return View(viewModel);
        }

        public async Task<IActionResult> Deposit()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            var viewModel = new DepositViewModel
            {
                Customer = customer
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(DepositViewModel viewModel)
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            viewModel.Customer = customer;

            if (viewModel.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Amount must be greater than zero.");
                viewModel.OperationStatus = OperationStatus.Failed;
                return View(viewModel);
            }

            var account = customer.Accounts.Find(x => x.AccountType == viewModel.AccountType);

            account.Balance += viewModel.Amount;
            account.Transactions.Add(new Transaction
            {
                TransactionType = TransactionType.Deposit,
                Amount = viewModel.Amount,
                ModifyDate = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            viewModel.OperationStatus = OperationStatus.Successful;
            viewModel.Amount = 0;
            return View(viewModel);
        }

        public async Task<IActionResult> Transfer()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            var viewModel = new WithdrawViewModel
            {
                Customer = customer
            };
            return View(viewModel);
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
