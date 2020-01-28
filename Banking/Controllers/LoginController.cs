using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SimpleHashing;

using Banking.Data;
using Banking.Models;
using Banking.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.Controllers
{
    public class LoginController : Controller
    {
        private readonly BankingContext _context;

        public IActionResult Index() => View();

        public LoginController(BankingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel viewModel)
        {
            var login = await _context.Login.FindAsync(viewModel.Login.UserID);
            viewModel.Login = login;

            viewModel.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(viewModel);

            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.Customer.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            return RedirectToAction("Index", "Customer");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
