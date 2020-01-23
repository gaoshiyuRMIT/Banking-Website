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

using X.PagedList;

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

            viewModel.Validate(ModelState);

            if (!ModelState.IsValid)
                return View(viewModel);

            var account = viewModel.Account;
            account.Balance -= viewModel.Amount;
            Transaction t = new Transaction
            {
                TransactionType = TransactionType.Withdrawal,
                Amount = viewModel.Amount,
                ModifyDate = DateTime.UtcNow
            };
            account.Transactions.Add(t);
            // deals with service fee
            if (t.ShouldCharge)
            {
                int nShouldCharge = account.Transactions.Where(x => x.ShouldCharge).Count();
                if (nShouldCharge > Transaction.NFreeTransaction)
                    account.Transactions.Add(t.CreateServiceTransaction());
            }
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

            viewModel.Validate(ModelState);

            if (!ModelState.IsValid)
                return View(viewModel);

            var account = viewModel.Account;

            account.Balance += viewModel.Amount;
            Transaction t = new Transaction
            {
                TransactionType = TransactionType.Deposit,
                Amount = viewModel.Amount,
                ModifyDate = DateTime.UtcNow
            };
            account.Transactions.Add(t);
            await _context.SaveChangesAsync();

            viewModel.OperationStatus = OperationStatus.Successful;
            viewModel.Amount = 0;
            return View(viewModel);
        }

        public async Task<IActionResult> Transfer()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            var viewModel = new TransferViewModel
            {
                Customer = customer
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel viewModel)
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            viewModel.Customer = customer;
            var destAccount = await _context.Account.FindAsync(viewModel.DestAccountNumber);
            viewModel.DestAccount = destAccount;

            viewModel.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(viewModel);

            var account = viewModel.Account;

            account.Balance -= viewModel.Amount;
            destAccount.Balance += viewModel.Amount;
            Transaction t = new Transaction
            {
                TransactionType = TransactionType.Transfer,
                Amount = viewModel.Amount,
                DestAccount = destAccount,
                ModifyDate = DateTime.UtcNow
            };
            if (t.ShouldCharge)
            {
                int nShouldCharge = account.Transactions.Where(x => x.ShouldCharge).Count();
                if (nShouldCharge > Transaction.NFreeTransaction)
                    account.Transactions.Add(t.CreateServiceTransaction());
            }
            await _context.SaveChangesAsync();

            viewModel.OperationStatus = OperationStatus.Successful;
            viewModel.Amount = 0;
            viewModel.DestAccountNumber = 0;
            return View(viewModel);
        }

        public async Task<IActionResult> Statements()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            var viewModel = new StatementsViewModel
            {
                Customer = customer
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Statements(StatementsViewModel viewModel)
        {
            var customer = await _context.Customer.FindAsync(CustomerID);
            viewModel.Customer = customer;

            viewModel.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(viewModel);

            const int pageSize = 4;
            var transactions = _context.Transaction
                .Where(x => x.AccountNumber == viewModel.Account.AccountNumber)
                .OrderByDescending<Transaction, DateTime>(x => x.ModifyDate);
            var pagedList = await transactions
                .ToPagedListAsync<Transaction>(viewModel.Page, pageSize);

            viewModel.Transactions = pagedList;

            return View(viewModel);
        }

        public async Task<IActionResult> Profile()
        {
            var customer = await _context.Customer.FindAsync(CustomerID);

            return View(customer);
        }

        [HttpPost]
        [Route("Profile/Edit")]
        public async Task<IActionResult> ProfileEdit(
            [Bind("Name,TFN,Address,City,State,PostCode,Phone")] Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            customer.CustomerID = CustomerID;
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", customer);
        }
    }
}
